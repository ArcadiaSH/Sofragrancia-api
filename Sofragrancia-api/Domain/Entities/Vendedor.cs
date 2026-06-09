namespace Sofragrancia_api.Domain.Entities;

public class Vendedor : BaseEntity
{
    public string TxNome { get; set; } = string.Empty;
    public string TxCpf { get; set; } = string.Empty;
    public string TxTelefone { get; set; } = string.Empty;
    public string TxEmail { get; set; } = string.Empty;
    public DateTime? DtAdmissao { get; set; }

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
