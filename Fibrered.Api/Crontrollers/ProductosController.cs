using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fibrered.Api.Data;

namespace Fibrered.Api.Controllers;

[ApiController]
[Route("api/productos")]
public sealed class ProductosController(FibreredDbContext db) : ControllerBase {
    // La lista entera de una sola vez, que alcanza para un corralon chico. Cuando pueda crecer sin
    // limite hay que paginar: eso es mas adelante.
    [HttpGet("resumen")]
    public async Task<IActionResult> Resumen(CancellationToken ct){
        // Los contadores se cuentan en la base, no sobre la lista devuelta: son dos preguntas
        // distintas y mezclarlas es lo que despues da numeros que no cierran.
        var total = await db.Productos.CountAsync(x => x.Activo, ct);
        var disponibles = await db.Productos.CountAsync(x => x.Activo && x.Disponible, ct);
        var resumen = new {Total = total, Disponibles = disponibles, NoDisponibles = total - disponibles};

        var categorias = await db.Categorias.AsNoTracking()
            .Where(x => x.Activo)
            .OrderBy(x => x.Orden)
            .Select(x => new {x.Id, x.Nombre, x.Orden})
            .ToListAsync(ct);

        // El join se resuelve en la base y baja el nombre de la categoria ya pegado a cada
        // producto: si no, el frontend tendria que cruzar las dos listas a mano.
        var productos = await (
            from producto in db.Productos.AsNoTracking()
            join categoria in db.Categorias.AsNoTracking() on producto.CategoriaId equals categoria.Id
            where producto.Activo
            orderby categoria.Orden, producto.Nombre
            select new {
                producto.Id,
                producto.Nombre,
                producto.Descripcion,
                producto.Precio,
                producto.ImagenUrl,
                producto.Disponible,
                CategoriaId = categoria.Id,
                CategoriaNombre = categoria.Nombre
            }
        ).ToListAsync(ct);

        return Ok(new {resumen, categorias, productos});
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Detalle(long id, CancellationToken ct){
        var producto = await (
            from p in db.Productos.AsNoTracking()
            join categoria in db.Categorias.AsNoTracking() on p.CategoriaId equals categoria.Id
            where p.Id == id && p.Activo
            select new {
                p.Id,
                p.Nombre,
                p.Descripcion,
                p.Precio,
                p.ImagenUrl,
                p.Disponible,
                CategoriaId = categoria.Id,
                CategoriaNombre = categoria.Nombre
            }
        ).FirstOrDefaultAsync(ct);
        // Un producto que no esta no es un error del servidor: es un 404 con un cuerpo que el
        // frontend puede mostrar tal cual.
        if(producto is null) return NotFound(new {codigo = "producto_no_encontrado", mensaje = "El producto no existe o fue dado de baja."});
        return Ok(new {producto});
    }

    // El endpoint del ejemplo en vivo. Es el unico que toca la columna stock, y por eso es el
    // unico que falla mientras la tercera migracion siga sin aplicar: MySQL contesta
    // "Unknown column p.stock". El resto de la API anda igual, porque ninguna otra consulta la pide.
    [HttpGet("stock")]
    public async Task<IActionResult> Stock(CancellationToken ct){
        var productos = await db.Productos.AsNoTracking()
            .Where(x => x.Activo)
            // Lo que menos queda, primero: es la lista con la que se sale a comprar.
            .OrderBy(x => x.Stock)
            .Select(x => new {x.Id, x.Nombre, x.Stock, x.Disponible})
            .ToListAsync(ct);
        return Ok(new {productos});
    }
}