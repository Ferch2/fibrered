using Microsoft.EntityFrameworkCore;
using Fibrered.Api.Data.Configurations;
using Fibrered.Api.Domain.Entities;

namespace Fibrered.Api.Data;

// Una propiedad DbSet por tabla: es lo que EF Core mira para saber que hay que crear, y lo que
// despues consultan los controllers.
public sealed class FibreredDbContext(DbContextOptions<FibreredDbContext> options) : DbContext(options) {
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new ProductoConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}