using Microsoft.EntityFrameworkCore;
using Sofragrancia_api.Domain.Entities;
using Sofragrancia_api.Domain.Interfaces;
using Sofragrancia_api.Infrastructure.Data;

namespace Sofragrancia_api.Infrastructure.Repositories;

public class EfRepository<T>(AppDbContext context) : IRepository<T> where T : BaseEntity
{
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(long id) =>
        await _dbSet.FindAsync(id);

    public async Task<T> AddAsync(T entity)
    {
        entity.DtCreatedate = DateTime.UtcNow;
        entity.DtUpdatedate = DateTime.UtcNow;
        _dbSet.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        entity.DtUpdatedate = DateTime.UtcNow;
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}

public class ClienteRepository(AppDbContext ctx) : EfRepository<Cliente>(ctx), IClienteRepository { }
public class VendedorRepository(AppDbContext ctx) : EfRepository<Vendedor>(ctx), IVendedorRepository { }
public class FornecedorRepository(AppDbContext ctx) : EfRepository<Fornecedor>(ctx), IFornecedorRepository { }
public class ProdutoRepository(AppDbContext ctx) : EfRepository<Produto>(ctx), IProdutoRepository { }
public class PedidoRepository(AppDbContext ctx) : EfRepository<Pedido>(ctx), IPedidoRepository { }
public class ItemPedidoRepository(AppDbContext ctx) : EfRepository<ItemPedido>(ctx), IItemPedidoRepository { }
public class PedidoDeCompraRepository(AppDbContext ctx) : EfRepository<PedidoDeCompra>(ctx), IPedidoDeCompraRepository { }
