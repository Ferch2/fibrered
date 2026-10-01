using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fibrered.Api.Domain.Entities;

namespace Fibrered.Api.Data.Configurations;

// Toda la Fluent API vive en este archivo: nombre de la tabla, largos, precision, indices y la
// relacion, en un solo lugar en vez de repartidos en atributos sobre las entidades.
// El nombre de cada columna no se escribe: lo saca EFCore.NamingConventions de la propiedad.

public sealed class CategoriaConfiguration : IEntityTypeConfiguration<Categoria> {
    public void Configure(EntityTypeBuilder<Categoria> builder){
        builder.ToTable("categorias");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
        builder.HasIndex(x => x.Nombre).IsUnique();
    }
}

public sealed class ProductoConfiguration : IEntityTypeConfiguration<Producto> {
    public void Configure(EntityTypeBuilder<Producto> builder){
        builder.ToTable("productos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Nombre).HasMaxLength(160).IsRequired();
        builder.Property(x => x.Descripcion).HasMaxLength(1000);
        // La plata nunca en double: decimal con escala fija, 12 enteros y 2 decimales.
        builder.Property(x => x.Precio).HasPrecision(12, 2);
        builder.Property(x => x.ImagenUrl).HasMaxLength(500);
        // Piezas en deposito. Entero y no decimal: se cuentan tirantes, no metros.
        builder.Property(x => x.Stock).HasDefaultValue(0);
        builder.HasIndex(x => new {x.CategoriaId, x.Nombre}).IsUnique();
        // Restrict: no se borra una categoria que todavia tiene productos colgando.
        builder.HasOne<Categoria>().WithMany().HasForeignKey(x => x.CategoriaId).OnDelete(DeleteBehavior.Restrict);
    }
}