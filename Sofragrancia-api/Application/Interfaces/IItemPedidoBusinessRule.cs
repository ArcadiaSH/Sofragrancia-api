namespace Sofragrancia_api.Application.Interfaces;

public interface IItemPedidoBusinessRule
{
    Task ValidarEAtualizarEstoqueAsync(long produtoId, int quantidade);
}
