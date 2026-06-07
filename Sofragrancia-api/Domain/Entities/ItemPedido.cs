namespace Sofragrancia_api.Domain.Entities;

public class ItemPedido : BaseEntity
{
    public long PedidoId { get; set; }
    public long PedidoClienteId { get; set; }
    public long PedidoVendedorId { get; set; }
    public long ProdutoId { get; set; }
    public int NrQuantidade { get; set; }
    public decimal NrPrecounitario { get; set; }
    public decimal NrDescontounitario { get; set; }
    public decimal NrSubtotal { get; set; }

    public Pedido Pedido { get; set; } = null!;
    public Produto Produto { get; set; } = null!;
}
