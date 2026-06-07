namespace Sofragrancia_api.Domain.Entities;

public class Pedido : BaseEntity
{
    public long ClienteId { get; set; }
    public long VendedorId { get; set; }
    public string NrPedido { get; set; } = string.Empty;
    public DateTime DtDatapedido { get; set; }
    public decimal NrValorbruto { get; set; }
    public decimal NrValordesconto { get; set; }
    public decimal NrValorliquido { get; set; }

    public Cliente Cliente { get; set; } = null!;
    public Vendedor Vendedor { get; set; } = null!;
    public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
}
