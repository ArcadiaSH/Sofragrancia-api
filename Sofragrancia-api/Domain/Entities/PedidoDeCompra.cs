namespace Sofragrancia_api.Domain.Entities;

public class PedidoDeCompra : BaseEntity
{
    public long ProdutoId { get; set; }
    public long FornecedorId { get; set; }
    public int NrQuantidade { get; set; }
    public string NrPrecounitario { get; set; } = string.Empty;
    public decimal NrDescontounitario { get; set; }
    public decimal Subtotal { get; set; }

    public Produto Produto { get; set; } = null!;
    public Fornecedor Fornecedor { get; set; } = null!;
}
