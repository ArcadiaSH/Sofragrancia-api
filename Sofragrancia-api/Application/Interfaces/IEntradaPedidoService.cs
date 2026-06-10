using Sofragrancia_api.Application.DTOs;

namespace Sofragrancia_api.Application.Interfaces;

public interface IEntradaPedidoService
{
    Task<EntradaPedidoDto> CreateAsync(EntradaPedidoCreateDto dto);
    Task<CancelamentoPedidoDto> CancelarAsync(string codigoPedido);
}
