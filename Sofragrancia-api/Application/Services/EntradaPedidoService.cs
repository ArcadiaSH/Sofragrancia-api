using Sofragrancia_api.Application.DTOs;
using Sofragrancia_api.Application.Interfaces;
using Sofragrancia_api.Domain.Entities;
using Sofragrancia_api.Domain.Interfaces;
using Sofragrancia_api.Infrastructure.Data;

namespace Sofragrancia_api.Application.Services;

public class EntradaPedidoService(
    AppDbContext context,
    IPedidoRepository pedidoRepository,
    IItemPedidoRepository itemPedidoRepository,
    IItemPedidoBusinessRule itemPedidoBusinessRule) : IEntradaPedidoService
{
    public async Task<EntradaPedidoDto> CreateAsync(EntradaPedidoCreateDto dto)
    {
        if (dto.ItensPedido is null || !dto.ItensPedido.Any())
            throw new InvalidOperationException("O pedido deve conter ao menos um item.");

        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var pedido = await pedidoRepository.AddAsync(new Pedido
            {
                ClienteId = dto.ClienteId,
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
                    throw new KeyNotFoundException($"Item {indiceItem} (ProdutoId: {itemDto.ProdutoId}) inválido: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException($"Item {indiceItem} (ProdutoId: {itemDto.ProdutoId}) não pôde ser inserido: {ex.Message}");
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
}
