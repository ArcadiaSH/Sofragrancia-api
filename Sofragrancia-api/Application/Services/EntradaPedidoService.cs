using System.Globalization;
using Sofragrancia_api.Application.DTOs;
using Sofragrancia_api.Application.Interfaces;
using Sofragrancia_api.Domain.Entities;
using Sofragrancia_api.Domain.Interfaces;
using Sofragrancia_api.Infrastructure.Data;

namespace Sofragrancia_api.Application.Services;

public class EntradaPedidoService(
    AppDbContext context,
    IClienteRepository clienteRepository,
    IPedidoRepository pedidoRepository,
    IItemPedidoRepository itemPedidoRepository,
    IReposicaoRepository reposicaoRepository,
    IProdutoRepository produtoRepository,
    IFornecedorRepository fornecedorRepository,
    IItemPedidoBusinessRule itemPedidoBusinessRule) : IEntradaPedidoService
{
    public async Task<EntradaPedidoDto> CreateAsync(EntradaPedidoCreateDto dto)
    {
        if (dto.Cliente is null)
            throw new InvalidOperationException("Os dados do cliente são obrigatórios para inserir o pedido.");

        if (dto.ItensPedido is null || !dto.ItensPedido.Any())
            throw new InvalidOperationException("O pedido deve conter ao menos um item.");

        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var codigoCliente = NormalizarCodigoCliente(dto.Cliente.TxCnpj);
            if (string.IsNullOrWhiteSpace(codigoCliente) && dto.Cliente.Id <= 0)
                throw new InvalidOperationException("Informe um cliente válido (Id ou CNPJ).");

            Cliente? clienteExistente = null;

            if (dto.Cliente.Id > 0)
                clienteExistente = await clienteRepository.GetByIdAsync(dto.Cliente.Id);

            if (clienteExistente is null && !string.IsNullOrWhiteSpace(codigoCliente))
            {
                var clientes = await clienteRepository.GetAllAsync();
                clienteExistente = clientes.FirstOrDefault(c => NormalizarCodigoCliente(c.TxCnpj) == codigoCliente);
            }

            var cliente = clienteExistente ?? await clienteRepository.AddAsync(new Cliente
            {
                Id = dto.Cliente.Id,
                TxRazaosocial = dto.Cliente.TxRazaosocial,
                TxNomefantasia = dto.Cliente.TxNomefantasia,
                TxCnpj = dto.Cliente.TxCnpj,
                TxTelefone = dto.Cliente.TxTelefone,
                TxEmail = dto.Cliente.TxEmail,
                TxEndereco = dto.Cliente.TxEndereco,
                TxCidade = dto.Cliente.TxCidade,
                TxEstado = dto.Cliente.TxEstado
            });

            var pedido = await pedidoRepository.AddAsync(new Pedido
            {
                ClienteId = cliente.Id,
                VendedorId = dto.VendedorId,
                NrPedido = dto.NrPedido,
                DtDatapedido = dto.DtDatapedido,
                NrValorbruto = dto.NrValorbruto,
                NrValordesconto = dto.NrValordesconto,
                NrValorliquido = dto.NrValorliquido
            });

            var itensCriados = new List<ItemPedidoDto>();
            var indiceItem = 0;

            foreach (var itemDto in dto.ItensPedido)
            {
                indiceItem++;

                try
                {
                    await itemPedidoBusinessRule.ValidarEAtualizarEstoqueAsync(itemDto.ProdutoId, itemDto.NrQuantidade);

                    var item = await itemPedidoRepository.AddAsync(new ItemPedido
                    {
                        PedidoId = pedido.Id,
                        PedidoClienteId = pedido.ClienteId,
                        PedidoVendedorId = pedido.VendedorId,
                        ProdutoId = itemDto.ProdutoId,
                        NrQuantidade = itemDto.NrQuantidade,
                        NrPrecounitario = itemDto.NrPrecounitario,
                        NrDescontounitario = itemDto.NrDescontounitario,
                        NrSubtotal = itemDto.NrSubtotal
                    });

                    itensCriados.Add(new ItemPedidoDto(item.Id, item.PedidoId, item.ProdutoId, item.NrQuantidade, item.NrPrecounitario, item.NrDescontounitario, item.NrSubtotal, item.FlIsenable));
                }
                catch (KeyNotFoundException ex)
                {
                    throw new KeyNotFoundException($"Não foi possível processar o item {indiceItem}: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException($"Não foi possível processar o item {indiceItem}: {ex.Message}");
                }
            }

            await transaction.CommitAsync();

            var pedidoDto = new PedidoDto(pedido.Id, pedido.ClienteId, pedido.VendedorId, pedido.NrPedido, pedido.DtDatapedido, pedido.NrValorbruto, pedido.NrValordesconto, pedido.NrValorliquido, pedido.FlIsenable);
            return new EntradaPedidoDto(pedidoDto, itensCriados);
        }
        catch (KeyNotFoundException ex)
        {
            await transaction.RollbackAsync();
            throw new KeyNotFoundException($"{ex.Message} Todas as inserções foram desfeitas (rollback).");
        }
        catch (InvalidOperationException ex)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException($"{ex.Message} Todas as inserções foram desfeitas (rollback).");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException("Erro ao inserir entrada de pedido. Todas as inserções foram desfeitas (rollback).");
        }
    }

    public async Task<CancelamentoPedidoDto> CancelarAsync(string codigoPedido)
    {
        if (string.IsNullOrWhiteSpace(codigoPedido))
            throw new InvalidOperationException("Informe o código do pedido para cancelar.");

        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var pedidos = await pedidoRepository.GetAllAsync();
            var pedido = pedidos.FirstOrDefault(p => p.NrPedido == codigoPedido)
                ?? throw new KeyNotFoundException("Pedido não encontrado para o código informado.");

            var itens = (await itemPedidoRepository.GetAllAsync())
                .Where(i => i.PedidoId == pedido.Id)
                .ToList();

            if (!itens.Any())
                throw new InvalidOperationException("Não há itens para cancelar neste pedido.");

            foreach (var item in itens)
            {
                var produto = await produtoRepository.GetByIdAsync(item.ProdutoId)
                    ?? throw new KeyNotFoundException("Produto do item do pedido não encontrado.");

                var fornecedorId = await ObterFornecedorParaReposicaoAsync(item.ProdutoId);

                var reposicaoCancelamento = new Reposicao
                {
                    ProdutoId = item.ProdutoId,
                    FornecedorId = fornecedorId,
                    NrQuantidade = item.NrQuantidade,
                    NrPrecounitario = item.NrPrecounitario.ToString(CultureInfo.InvariantCulture),
                    NrDescontounitario = item.NrDescontounitario,
                    Subtotal = item.NrSubtotal,
                    Tipo = "Pedido cancelado"
                };

                await reposicaoRepository.AddAsync(reposicaoCancelamento);

                produto.NrEstoqueatual += item.NrQuantidade;
                await produtoRepository.UpdateAsync(produto);

                await itemPedidoRepository.DeleteAsync(item.Id);
            }

            await pedidoRepository.DeleteAsync(pedido.Id);
            await transaction.CommitAsync();

            return new CancelamentoPedidoDto(
                pedido.NrPedido,
                itens.Count,
                "Pedido cancelado com sucesso. O estoque dos produtos foi recomposto.");
        }
        catch (KeyNotFoundException ex)
        {
            await transaction.RollbackAsync();
            throw new KeyNotFoundException($"{ex.Message} Nenhuma alteração foi aplicada.");
        }
        catch (InvalidOperationException ex)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException($"{ex.Message} Nenhuma alteração foi aplicada.");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException("Erro ao cancelar pedido. Nenhuma alteração foi aplicada.");
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
            throw new InvalidOperationException("Não foi possível cancelar o pedido: nenhum fornecedor cadastrado para gerar reposição.");

        return primeiroFornecedor.Id;
    }

    private static string NormalizarCodigoCliente(string? codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            return string.Empty;

        return new string(codigo.Where(char.IsDigit).ToArray());
    }
}
