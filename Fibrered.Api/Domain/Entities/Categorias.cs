namespace Fibrered.Api.Domain.Entities;

// Rubro del corralon: Maderas, Placas, Insumos. Orden decide como se listan en la app.
public sealed class Categoria : EntityBase {
    public string Nombre { get; set; } = string.Empty;
    public int Orden { get; set; }
    public bool Activo { get; set; } = true;
}