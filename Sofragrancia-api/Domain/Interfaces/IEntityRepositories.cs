using Sofragrancia_api.Domain.Entities;
namespace Sofragrancia_api.Domain.Interfaces;
public interface IClienteRepository : IRepository<Cliente> { }
public interface IVendedorRepository : IRepository<Vendedor> { }
public interface IFornecedorRepository : IRepository<Fornecedor> { }
public interface IProdutoRepository : IRepository<Produto> { }
public interface IPedidoRepository : IRepository<Pedido> { }
public interface IItemPedidoRepository : IRepository<ItemPedido> { }
public interface IReposicaoRepository : IRepository<Reposicao> { }
