using Sofragrancia_api.Application.Interfaces;
using Sofragrancia_api.Domain.Entities;
using Sofragrancia_api.Domain.Interfaces;

namespace Sofragrancia_api.Application.Services;

public class ItemPedidoBusinessRule(
    IProdutoRepository produtoRepository,
    IReposicaoRepository reposicaoRepository,
    IFornecedorRepository fornecedorRepository) : IItemPedidoBusinessRule
{
    public async Task ValidarEAtualizarEstoqueAsync(long produtoId, int quantidade)
    {
        var produto = await produtoRepository.GetByIdAsync(produtoId) ?? throw new KeyNotFoundException("Produto não encontrado.");

        if (quantidade > produto.NrEstoqueatual)
            throw new InvalidOperationException("Quantidade solicitada maior que o estoque atual do produto.");

        produto.NrEstoqueatual -= quantidade;
        await produtoRepository.UpdateAsync(produto);

        if (produto.NrEstoqueatual < produto.NrEstoqueminimo)
        {
            var fornecedorId = await ObterFornecedorParaReposicaoAsync(produtoId);

            var reposicaoManual = new Reposicao
            {
                ProdutoId = produtoId,
                FornecedorId = fornecedorId,
                NrQuantidade = produto.NrEstoqueminimo,
                NrPrecounitario = "0",
                NrDescontounitario = 0,
                Subtotal = 0,
                Tipo = "Manual"
            };

            await reposicaoRepository.AddAsync(reposicaoManual);
        }
    }

    private async Task<long> ObterFornecedorParaReposicaoAsync(long produtoId)
    {
        var reposicoesDoProduto = await reposicaoRepository.GetAllAsync();
        var fornecedorDaUltimaReposicao = reposicoesDoProduto
            .Where(r => r.ProdutoId == produtoId)
            .OrderByDescending(r => r.DtCreatedate)
            .Select(r => r.FornecedorId)
            .FirstOrDefault();

        if (fornecedorDaUltimaReposicao > 0)
            return fornecedorDaUltimaReposicao;

        var primeiroFornecedor = (await fornecedorRepository.GetAllAsync()).FirstOrDefault();
        if (primeiroFornecedor is null)
            throw new InvalidOperationException("Não foi possível criar reposição manual: nenhum fornecedor cadastrado.");

        return primeiroFornecedor.Id;
    }
}
