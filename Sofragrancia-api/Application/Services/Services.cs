using Sofragrancia_api.Application.DTOs;
using Sofragrancia_api.Application.Interfaces;
using Sofragrancia_api.Domain.Entities;
using Sofragrancia_api.Domain.Interfaces;

namespace Sofragrancia_api.Application.Services;

public class ClienteService(IClienteRepository repo) : IService<ClienteDto, ClienteCreateDto>
{
    public async Task<IEnumerable<ClienteDto>> GetAllAsync() =>
        (await repo.GetAllAsync()).Select(c => new ClienteDto(c.Id, c.TxRazaosocial, c.TxNomefantasia, c.TxCnpj, c.TxTelefone, c.TxEmail, c.TxEndereco, c.TxCidade, c.TxEstado, c.FlIsenable));

    public async Task<ClienteDto?> GetByIdAsync(long id)
    {
        var c = await repo.GetByIdAsync(id);
        return c is null ? null : new ClienteDto(c.Id, c.TxRazaosocial, c.TxNomefantasia, c.TxCnpj, c.TxTelefone, c.TxEmail, c.TxEndereco, c.TxCidade, c.TxEstado, c.FlIsenable);
    }

    public async Task<ClienteDto> CreateAsync(ClienteCreateDto dto)
    {
        var entity = new Cliente { TxRazaosocial = dto.TxRazaosocial, TxNomefantasia = dto.TxNomefantasia, TxCnpj = dto.TxCnpj, TxTelefone = dto.TxTelefone, TxEmail = dto.TxEmail, TxEndereco = dto.TxEndereco, TxCidade = dto.TxCidade, TxEstado = dto.TxEstado };
        var c = await repo.AddAsync(entity);
        return new ClienteDto(c.Id, c.TxRazaosocial, c.TxNomefantasia, c.TxCnpj, c.TxTelefone, c.TxEmail, c.TxEndereco, c.TxCidade, c.TxEstado, c.FlIsenable);
    }

    public async Task UpdateAsync(long id, ClienteCreateDto dto)
    {
        var c = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        c.TxRazaosocial = dto.TxRazaosocial; c.TxNomefantasia = dto.TxNomefantasia; c.TxCnpj = dto.TxCnpj;
        c.TxTelefone = dto.TxTelefone; c.TxEmail = dto.TxEmail; c.TxEndereco = dto.TxEndereco;
        c.TxCidade = dto.TxCidade; c.TxEstado = dto.TxEstado; c.DtUpdatedate = DateTime.UtcNow;
        await repo.UpdateAsync(c);
    }

    public async Task DeleteAsync(long id) => await repo.DeleteAsync(id);
}

public class VendedorService(IVendedorRepository repo) : IService<VendedorDto, VendedorCreateDto>
{
    public async Task<IEnumerable<VendedorDto>> GetAllAsync() =>
        (await repo.GetAllAsync()).Select(v => new VendedorDto(v.Id, v.TxNome, v.TxCpf, v.TxTelefone, v.TxEmail, v.DtAdmissao ?? DateTime.MinValue, v.FlIsenable));

    public async Task<VendedorDto?> GetByIdAsync(long id)
    {
        var v = await repo.GetByIdAsync(id);
        return v is null ? null : new VendedorDto(v.Id, v.TxNome, v.TxCpf, v.TxTelefone, v.TxEmail, v.DtAdmissao ?? DateTime.MinValue, v.FlIsenable);
    }

    public async Task<VendedorDto> CreateAsync(VendedorCreateDto dto)
    {
        var entity = new Vendedor {Id = dto.id, TxNome = dto.TxNome, TxCpf = dto.TxCpf, TxTelefone = dto.TxTelefone, TxEmail = dto.TxEmail, DtAdmissao = dto.DtAdmissao };
        var v = await repo.AddAsync(entity);
        return new VendedorDto(v.Id, v.TxNome, v.TxCpf, v.TxTelefone, v.TxEmail, v.DtAdmissao ?? DateTime.MinValue, v.FlIsenable);
    }

    public async Task UpdateAsync(long id, VendedorCreateDto dto)
    {
        var v = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        v.TxNome = dto.TxNome; v.TxCpf = dto.TxCpf; v.TxTelefone = dto.TxTelefone; v.TxEmail = dto.TxEmail; v.DtAdmissao = dto.DtAdmissao; v.DtUpdatedate = DateTime.UtcNow;
        await repo.UpdateAsync(v);
    }

    public async Task DeleteAsync(long id) => await repo.DeleteAsync(id);
}

public class FornecedorService(IFornecedorRepository repo) : IService<FornecedorDto, FornecedorCreateDto>
{
    public async Task<IEnumerable<FornecedorDto>> GetAllAsync() =>
        (await repo.GetAllAsync()).Select(f => new FornecedorDto(f.Id, f.TxCod, f.TxRazaosocial, f.TxNomefantasia, f.TxCnpj, f.TxTelefone, f.TxEmail, f.TxEndereco, f.TxCidade, f.TxEstado, f.FlIsenable));

    public async Task<FornecedorDto?> GetByIdAsync(long id)
    {
        var f = await repo.GetByIdAsync(id);
        return f is null ? null : new FornecedorDto(f.Id, f.TxCod, f.TxRazaosocial, f.TxNomefantasia, f.TxCnpj, f.TxTelefone, f.TxEmail, f.TxEndereco, f.TxCidade, f.TxEstado, f.FlIsenable);
    }

    public async Task<FornecedorDto> CreateAsync(FornecedorCreateDto dto)
    {
        var entity = new Fornecedor { TxCod = dto.TxCod, TxRazaosocial = dto.TxRazaosocial, TxNomefantasia = dto.TxNomefantasia, TxCnpj = dto.TxCnpj, TxTelefone = dto.TxTelefone, TxEmail = dto.TxEmail, TxEndereco = dto.TxEndereco, TxCidade = dto.TxCidade, TxEstado = dto.TxEstado };
        var f = await repo.AddAsync(entity);
        return new FornecedorDto(f.Id, f.TxCod, f.TxRazaosocial, f.TxNomefantasia, f.TxCnpj, f.TxTelefone, f.TxEmail, f.TxEndereco, f.TxCidade, f.TxEstado, f.FlIsenable);
    }

    public async Task UpdateAsync(long id, FornecedorCreateDto dto)
    {
        var f = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        f.TxCod = dto.TxCod; f.TxRazaosocial = dto.TxRazaosocial; f.TxNomefantasia = dto.TxNomefantasia; f.TxCnpj = dto.TxCnpj;
        f.TxTelefone = dto.TxTelefone; f.TxEmail = dto.TxEmail; f.TxEndereco = dto.TxEndereco;
        f.TxCidade = dto.TxCidade; f.TxEstado = dto.TxEstado; f.DtUpdatedate = DateTime.UtcNow;
        await repo.UpdateAsync(f);
    }

    public async Task DeleteAsync(long id) => await repo.DeleteAsync(id);
}

public class ProdutoService(IProdutoRepository repo) : IService<ProdutoDto, ProdutoCreateDto>
{
    public async Task<IEnumerable<ProdutoDto>> GetAllAsync() =>
        (await repo.GetAllAsync()).Select(p => new ProdutoDto(p.Id, p.TxDescricao ?? string.Empty, p.TxUnidade ?? string.Empty, p.NrPrecocusto, p.NrPrecovenda, p.NrEstoqueatual, p.NrEstoqueminimo, p.FlIsenable));

    public async Task<ProdutoDto?> GetByIdAsync(long id)
    {
        var p = await repo.GetByIdAsync(id);
        return p is null ? null : new ProdutoDto(p.Id, p.TxDescricao ?? string.Empty, p.TxUnidade ?? string.Empty, p.NrPrecocusto, p.NrPrecovenda, p.NrEstoqueatual, p.NrEstoqueminimo, p.FlIsenable);
    }

    public async Task<ProdutoDto> CreateAsync(ProdutoCreateDto dto)
    {
        var entity = new Produto { TxDescricao = dto.TxDescricao, TxUnidade = dto.TxUnidade, NrPrecocusto = dto.NrPrecocusto, NrPrecovenda = dto.NrPrecovenda, NrEstoqueatual = dto.NrEstoqueatual, NrEstoqueminimo = dto.NrEstoqueminimo };
        var p = await repo.AddAsync(entity);
        return new ProdutoDto(p.Id, p.TxDescricao, p.TxUnidade, p.NrPrecocusto, p.NrPrecovenda, p.NrEstoqueatual, p.NrEstoqueminimo, p.FlIsenable);
    }

    public async Task UpdateAsync(long id, ProdutoCreateDto dto)
    {
        var p = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        p.TxDescricao = dto.TxDescricao; p.TxUnidade = dto.TxUnidade; p.NrPrecocusto = dto.NrPrecocusto;
        p.NrPrecovenda = dto.NrPrecovenda; p.NrEstoqueatual = dto.NrEstoqueatual; p.NrEstoqueminimo = dto.NrEstoqueminimo; p.DtUpdatedate = DateTime.UtcNow;
        await repo.UpdateAsync(p);
    }

    public async Task DeleteAsync(long id) => await repo.DeleteAsync(id);
}

public class PedidoService(IPedidoRepository repo) : IService<PedidoDto, PedidoCreateDto>
{
    public async Task<IEnumerable<PedidoDto>> GetAllAsync() =>
        (await repo.GetAllAsync()).Select(p => new PedidoDto(p.Id, p.ClienteId, p.VendedorId, p.NrPedido, p.DtDatapedido, p.NrValorbruto, p.NrValordesconto, p.NrValorliquido, p.FlIsenable));

    public async Task<PedidoDto?> GetByIdAsync(long id)
    {
        var p = await repo.GetByIdAsync(id);
        return p is null ? null : new PedidoDto(p.Id, p.ClienteId, p.VendedorId, p.NrPedido, p.DtDatapedido, p.NrValorbruto, p.NrValordesconto, p.NrValorliquido, p.FlIsenable);
    }

    public async Task<PedidoDto> CreateAsync(PedidoCreateDto dto)
    {
        var entity = new Pedido { ClienteId = dto.ClienteId, VendedorId = dto.VendedorId, NrPedido = dto.NrPedido, DtDatapedido = dto.DtDatapedido, NrValorbruto = dto.NrValorbruto, NrValordesconto = dto.NrValordesconto, NrValorliquido = dto.NrValorliquido };
        var p = await repo.AddAsync(entity);
        return new PedidoDto(p.Id, p.ClienteId, p.VendedorId, p.NrPedido, p.DtDatapedido, p.NrValorbruto, p.NrValordesconto, p.NrValorliquido, p.FlIsenable);
    }

    public async Task UpdateAsync(long id, PedidoCreateDto dto)
    {
        var p = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        p.ClienteId = dto.ClienteId; p.VendedorId = dto.VendedorId; p.NrPedido = dto.NrPedido;
        p.DtDatapedido = dto.DtDatapedido; p.NrValorbruto = dto.NrValorbruto; p.NrValordesconto = dto.NrValordesconto; p.NrValorliquido = dto.NrValorliquido; p.DtUpdatedate = DateTime.UtcNow;
        await repo.UpdateAsync(p);
    }

    public async Task DeleteAsync(long id) => await repo.DeleteAsync(id);
}

public class ItemPedidoService(IItemPedidoRepository repo) : IService<ItemPedidoDto, ItemPedidoCreateDto>
{
    public async Task<IEnumerable<ItemPedidoDto>> GetAllAsync() =>
        (await repo.GetAllAsync()).Select(i => new ItemPedidoDto(i.Id, i.PedidoId, i.ProdutoId, i.NrQuantidade, i.NrPrecounitario, i.NrDescontounitario, i.NrSubtotal, i.FlIsenable));

    public async Task<ItemPedidoDto?> GetByIdAsync(long id)
    {
        var i = await repo.GetByIdAsync(id);
        return i is null ? null : new ItemPedidoDto(i.Id, i.PedidoId, i.ProdutoId, i.NrQuantidade, i.NrPrecounitario, i.NrDescontounitario, i.NrSubtotal, i.FlIsenable);
    }

    public async Task<ItemPedidoDto> CreateAsync(ItemPedidoCreateDto dto)
    {
        var entity = new ItemPedido { PedidoId = dto.PedidoId, PedidoClienteId = dto.PedidoClienteId, PedidoVendedorId = dto.PedidoVendedorId, ProdutoId = dto.ProdutoId, NrQuantidade = dto.NrQuantidade, NrPrecounitario = dto.NrPrecounitario, NrDescontounitario = dto.NrDescontounitario, NrSubtotal = dto.NrSubtotal };
        var i = await repo.AddAsync(entity);
        return new ItemPedidoDto(i.Id, i.PedidoId, i.ProdutoId, i.NrQuantidade, i.NrPrecounitario, i.NrDescontounitario, i.NrSubtotal, i.FlIsenable);
    }

    public async Task UpdateAsync(long id, ItemPedidoCreateDto dto)
    {
        var i = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        i.PedidoId = dto.PedidoId; i.ProdutoId = dto.ProdutoId; i.NrQuantidade = dto.NrQuantidade;
        i.NrPrecounitario = dto.NrPrecounitario; i.NrDescontounitario = dto.NrDescontounitario; i.NrSubtotal = dto.NrSubtotal; i.DtUpdatedate = DateTime.UtcNow;
        await repo.UpdateAsync(i);
    }

    public async Task DeleteAsync(long id) => await repo.DeleteAsync(id);
}

public class ReposicaoService(IReposicaoRepository repo) : IService<ReposicaoDto, ReposicaoCreateDto>
{
    public async Task<IEnumerable<ReposicaoDto>> GetAllAsync() =>
        (await repo.GetAllAsync()).Select(r => new ReposicaoDto(r.Id, r.ProdutoId, r.FornecedorId, r.NrQuantidade, r.NrPrecounitario, r.NrDescontounitario, r.Subtotal, r.FlIsenable));

    public async Task<ReposicaoDto?> GetByIdAsync(long id)
    {
        var r = await repo.GetByIdAsync(id);
        return r is null ? null : new ReposicaoDto(r.Id, r.ProdutoId, r.FornecedorId, r.NrQuantidade, r.NrPrecounitario, r.NrDescontounitario, r.Subtotal, r.FlIsenable);
    }

    public async Task<ReposicaoDto> CreateAsync(ReposicaoCreateDto dto)
    {
        var entity = new Reposicao { ProdutoId = dto.ProdutoId, FornecedorId = dto.FornecedorId, NrQuantidade = dto.NrQuantidade, NrPrecounitario = dto.NrPrecounitario, NrDescontounitario = dto.NrDescontounitario, Subtotal = dto.Subtotal };
        var r = await repo.AddAsync(entity);
        return new ReposicaoDto(r.Id, r.ProdutoId, r.FornecedorId, r.NrQuantidade, r.NrPrecounitario, r.NrDescontounitario, r.Subtotal, r.FlIsenable);
    }

    public async Task UpdateAsync(long id, ReposicaoCreateDto dto)
    {
        var r = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        r.ProdutoId = dto.ProdutoId; r.FornecedorId = dto.FornecedorId; r.NrQuantidade = dto.NrQuantidade;
        r.NrPrecounitario = dto.NrPrecounitario; r.NrDescontounitario = dto.NrDescontounitario; r.Subtotal = dto.Subtotal; r.DtUpdatedate = DateTime.UtcNow;
        await repo.UpdateAsync(r);
    }

    public async Task DeleteAsync(long id) => await repo.DeleteAsync(id);
}
