using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Fibrered.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// La cadena real vive en User Secrets, fuera del repositorio. appsettings.json solo trae la
// plantilla con Password=CAMBIAR: si el secreto no se cargo, MySQL corta con "Access denied".
var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("No se configuro ConnectionStrings:Default.");
const string corsPolicy = "IonicDevelopment";

builder.Services.AddControllers().AddJsonOptions(options =>
    // El JSON que intercambian API y frontend va en snake_case, igual que las columnas de MySQL:
    // una sola convencion de punta a punta evita mapear a mano en cada service del front.
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

// UseSnakeCaseNamingConvention viene de EFCore.NamingConventions: es lo que hace que la propiedad
// CategoriaId termine en la columna categoria_id sin escribir un HasColumnName por cada una.
builder.Services.AddDbContext<CarpinteriaDbContext>(options => options
    .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0)))
    .UseSnakeCaseNamingConvention());

// Quien puede llamar a la API desde un navegador. Sin esto, el navegador bloquea la respuesta
// aunque la API haya contestado bien.
builder.Services.AddCors(options => options.AddPolicy(corsPolicy,
    policy => policy.SetIsOriginAllowed(EsOrigenDesarrolloPermitido).AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors(corsPolicy);
// Sin esto wwwroot no se sirve y todas las imagenes dan 404.
app.UseStaticFiles();

// dotnet run -- --migrate: aplica las migraciones pendientes y despues queda escuchando. No borra
// nada; es lo mismo que "database update" pero sin el paso aparte.
if(args.Contains("--migrate", StringComparer.OrdinalIgnoreCase)){
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<CarpinteriaDbContext>();
    await db.Database.MigrateAsync();
}

app.MapControllers();
// El primer endpoint que se prueba: si /health no responde, no hay nada mas que revisar del lado
// del frontend. Tambien es lo que confirma que el telefono llega hasta la maquina de desarrollo.
app.MapGet("/health", () => Results.Ok(new {status = "ok"}));
app.Run();

// En desarrollo el origen no se puede listar a mano: el telefono entra por la IP de la red local,
// que cambia de casa en casa. Se acepta localhost y cualquier IP de red privada; nunca una
// direccion publica.
static bool EsOrigenDesarrolloPermitido(string origen){
    if(!Uri.TryCreate(origen, UriKind.Absolute, out var uri)) return false;
    if(uri.Host is "localhost" or "127.0.0.1") return true;
    if(!IPAddress.TryParse(uri.Host, out var ip)) return false;
    var partes = ip.GetAddressBytes();
    // Los tres rangos privados de IPv4: 10.x.x.x, 192.168.x.x y 172.16.x.x a 172.31.x.x.
    return partes[0] == 10
        || (partes[0] == 192 && partes[1] == 168)
        || (partes[0] == 172 && partes[1] >= 16 && partes[1] <= 31);
}