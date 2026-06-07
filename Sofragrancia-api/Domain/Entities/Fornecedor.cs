namespace Sofragrancia_api.Domain.Entities;

public class Fornecedor : BaseEntity
{
    public string TxRazaosocial { get; set; } = string.Empty;
    public string TxNomefantasia { get; set; } = string.Empty;
    public string TxCnpj { get; set; } = string.Empty;
    public string TxTelefone { get; set; } = string.Empty;
    public string TxEmail { get; set; } = string.Empty;
    public string TxEndereco { get; set; } = string.Empty;
    public string TxCidade { get; set; } = string.Empty;
    public string TxEstado { get; set; } = string.Empty;

    public ICollection<PedidoDeCompra> PedidosDeCompra { get; set; } = new List<PedidoDeCompra>();
}
