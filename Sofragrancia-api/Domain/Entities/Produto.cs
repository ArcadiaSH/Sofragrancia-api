namespace Sofragrancia_api.Domain.Entities;

public class Produto : BaseEntity
{
    public string TxDescricao { get; set; } = string.Empty;
    public string TxUnidade { get; set; } = string.Empty;
    public decimal NrPrecocusto { get; set; }
    public decimal NrPrecovenda { get; set; }
    public int NrEstoqueatual { get; set; }
    public int NrEstoqueminimo { get; set; }

    public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    public ICollection<Reposicao> Reposicoes { get; set; } = new List<Reposicao>();
}
