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
        if (quantidade <= 0)
            throw new InvalidOperationException("A quantidade informada para o item deve ser maior que zero.");

        var produto = await produtoRepository.GetByIdAsync(produtoId) ?? throw new KeyNotFoundException("Não foi possível localizar o produto informado.");
        var descricaoProduto = string.IsNullOrWhiteSpace(produto.TxDescricao) ? "produto selecionado" : produto.TxDescricao;

        if (produto.NrEstoqueatual <= 0)
            throw new InvalidOperationException($"No momento, não temos estoque disponível para \"{descricaoProduto}\".");

        if (quantidade > produto.NrEstoqueatual)
            throw new InvalidOperationException($"Estoque insuficiente para \"{descricaoProduto}\". Disponível: {produto.NrEstoqueatual} unidade(s).");

        produto.NrEstoqueatual -= quantidade;

        if (produto.NrEstoqueatual <= produto.NrEstoqueminimo)
        {
            var fornecedorId = await ObterFornecedorParaReposicaoAsync(produtoId);

            var reposicaoAutomatica = new Reposicao
            {
                ProdutoId = produtoId,
                FornecedorId = fornecedorId,
                NrQuantidade = produto.NrEstoqueminimo,
                NrPrecounitario = "0",
                NrDescontounitario = 0,
                Subtotal = produto.NrPrecocusto * produto.NrEstoqueminimo,
                Tipo = "automatica"
            };

            await reposicaoRepository.AddAsync(reposicaoAutomatica);
            produto.NrEstoqueatual += reposicaoAutomatica.NrQuantidade;
        }

        await produtoRepository.UpdateAsync(produto);
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
