namespace Sofragrancia_api.Application.Interfaces;

public interface IReposicaoBusinessRule
{
    Task AtualizarEstoqueProdutoAsync(long produtoId, int quantidade);
}
