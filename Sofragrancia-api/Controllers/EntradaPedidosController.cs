using Microsoft.AspNetCore.Mvc;
using Sofragrancia_api.Application.DTOs;
using Sofragrancia_api.Application.Interfaces;

namespace Sofragrancia_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntradaPedidosController(IEntradaPedidoService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<EntradaPedidoDto>> Create([FromBody] EntradaPedidoCreateDto dto)
    {
        try
        {
            var result = await service.CreateAsync(dto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("cancelar/{codigoPedido}")]
    public async Task<ActionResult<CancelamentoPedidoDto>> Cancelar(string codigoPedido)
    {
        try
        {
            var result = await service.CancelarAsync(codigoPedido);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
