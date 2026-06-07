namespace Sofragrancia_api.Domain.Entities;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTime DtCreatedate { get; set; } = DateTime.UtcNow;
    public DateTime DtUpdatedate { get; set; } = DateTime.UtcNow;
    public bool FlIsenable { get; set; } = true;
}
