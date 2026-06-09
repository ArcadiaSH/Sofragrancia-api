using Sofragrancia_api.Application.Interfaces;
using Sofragrancia_api.Domain.Interfaces;

namespace Sofragrancia_api.Application.Services;

public class ReposicaoBusinessRule(IProdutoRepository produtoRepository) : IReposicaoBusinessRule
{
    public async Task AtualizarEstoqueProdutoAsync(long produtoId, int quantidade)
    {
        var produto = await produtoRepository.GetByIdAsync(produtoId) ?? throw new KeyNotFoundException();
        produto.NrEstoqueatual += quantidade;
        await produtoRepository.UpdateAsync(produto);
    }
}
