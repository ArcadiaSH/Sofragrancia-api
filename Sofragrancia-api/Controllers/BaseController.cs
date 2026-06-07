using Microsoft.AspNetCore.Mvc;
using Sofragrancia_api.Application.Interfaces;

namespace Sofragrancia_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<TDto, TCreateDto>(IService<TDto, TCreateDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TDto>>> GetAll() =>
        Ok(await service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<TDto>> GetById(long id)
    {
        var result = await service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateDto dto)
    {
        var result = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = 0 }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] TCreateDto dto)
    {
        try { await service.UpdateAsync(id, dto); return NoContent(); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
