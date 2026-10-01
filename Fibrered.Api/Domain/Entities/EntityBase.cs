namespace Fibrered.Api.Domain.Entities;

// Id y fechas, comunes a las dos entidades. Estan aca y no repetidas en cada clase: si manana se
// agrega una tercera tabla, hereda de esta y ya tiene las tres columnas.
public abstract class EntityBase {
    public long Id { get; set; }
    public DateTimeOffset CreadoEn { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ActualizadoEn { get; set; } = DateTimeOffset.UtcNow;
}