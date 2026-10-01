using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fibrered.Api.Data;

namespace Fibrered.Api.Controllers;

[ApiController]
[Route("api/categorias")]
public sealed class CategoriasController(FibreredDbContext db) : ControllerBase {
    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct){
        // El Select es lo importante: la respuesta nunca es la entidad entera. Asi no salen las
        // fechas de auditoria ni el flag Activo, que son cosa de la base y no del frontend.
        // Las propiedades van en PascalCase y el JSON sale en snake_case: lo traduce Program.cs.
        var categorias = await db.Categorias.AsNoTracking()
            .Where(x => x.Activo)
            .OrderBy(x => x.Orden)
            .Select(x => new {x.Id, x.Nombre, x.Orden})
            .ToListAsync(ct);
        return Ok(new {categorias});
    }
}