namespace Sofragrancia_api.Application.Interfaces;

public interface IService<TDto, TCreateDto>
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(long id);
    Task<TDto> CreateAsync(TCreateDto dto);
    Task UpdateAsync(long id, TCreateDto dto);
    Task DeleteAsync(long id);
}
