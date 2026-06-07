using Sofragrancia_api.Application.DTOs;
using Sofragrancia_api.Application.Interfaces;

namespace Sofragrancia_api.Controllers;

public class ClientesController(IService<ClienteDto, ClienteCreateDto> s) : BaseController<ClienteDto, ClienteCreateDto>(s) { }
public class VendedoresController(IService<VendedorDto, VendedorCreateDto> s) : BaseController<VendedorDto, VendedorCreateDto>(s) { }
public class FornecedoresController(IService<FornecedorDto, FornecedorCreateDto> s) : BaseController<FornecedorDto, FornecedorCreateDto>(s) { }
public class ProdutosController(IService<ProdutoDto, ProdutoCreateDto> s) : BaseController<ProdutoDto, ProdutoCreateDto>(s) { }
public class PedidosController(IService<PedidoDto, PedidoCreateDto> s) : BaseController<PedidoDto, PedidoCreateDto>(s) { }
public class ItensPedidoController(IService<ItemPedidoDto, ItemPedidoCreateDto> s) : BaseController<ItemPedidoDto, ItemPedidoCreateDto>(s) { }
public class ReposicoesController(IService<ReposicaoDto, ReposicaoCreateDto> s) : BaseController<ReposicaoDto, ReposicaoCreateDto>(s) { }
