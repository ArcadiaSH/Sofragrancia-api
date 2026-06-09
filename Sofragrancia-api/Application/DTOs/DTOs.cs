namespace Sofragrancia_api.Application.DTOs;

public record ClienteDto(long Id, string TxRazaosocial, string TxNomefantasia, string TxCnpj, string TxTelefone, string TxEmail, string TxEndereco, string TxCidade, string TxEstado, bool? FlIsenable);
public record ClienteCreateDto(string TxRazaosocial, string TxNomefantasia, string TxCnpj, string TxTelefone, string TxEmail, string TxEndereco, string TxCidade, string TxEstado);

public record VendedorDto(long Id, string TxNome, string TxCpf, string TxTelefone, string TxEmail, DateTime? DtAdmissao, bool? FlIsenable);
public record VendedorCreateDto(long id, string TxNome, string TxCpf, string TxTelefone, string TxEmail, DateTime? DtAdmissao);

public record FornecedorDto(long Id, string TxCod, string TxRazaosocial, string TxNomefantasia, string TxCnpj, string TxTelefone, string TxEmail, string TxEndereco, string TxCidade, string TxEstado, bool? FlIsenable);
public record FornecedorCreateDto(string TxCod, string TxRazaosocial, string TxNomefantasia, string TxCnpj, string TxTelefone, string TxEmail, string TxEndereco, string TxCidade, string TxEstado);

public record ProdutoDto(long Id, string TxDescricao, string TxUnidade, decimal NrPrecocusto, decimal NrPrecovenda, int NrEstoqueatual, int NrEstoqueminimo, bool? FlIsenable);
public record ProdutoCreateDto(string TxDescricao, string TxUnidade, decimal NrPrecocusto, decimal NrPrecovenda, int NrEstoqueatual, int NrEstoqueminimo);

public record PedidoDto(long Id, long ClienteId, long VendedorId, string NrPedido, DateTime DtDatapedido, decimal NrValorbruto, decimal NrValordesconto, decimal NrValorliquido, bool? FlIsenable);
public record PedidoCreateDto(long ClienteId, long VendedorId, string NrPedido, DateTime DtDatapedido, decimal NrValorbruto, decimal NrValordesconto, decimal NrValorliquido);

public record ItemPedidoDto(long Id, long PedidoId, long ProdutoId, int NrQuantidade, decimal NrPrecounitario, decimal NrDescontounitario, decimal NrSubtotal, bool? FlIsenable);
public record ItemPedidoCreateDto(long PedidoId, long PedidoClienteId, long PedidoVendedorId, long ProdutoId, int NrQuantidade, decimal NrPrecounitario, decimal NrDescontounitario, decimal NrSubtotal);

public record ReposicaoDto(long Id, long ProdutoId, long FornecedorId, int NrQuantidade, string NrPrecounitario, decimal NrDescontounitario, decimal Subtotal, string Tipo, bool? FlIsenable);
public record ReposicaoCreateDto(long ProdutoId, long FornecedorId, int NrQuantidade, string NrPrecounitario, decimal NrDescontounitario, decimal Subtotal, string Tipo);
