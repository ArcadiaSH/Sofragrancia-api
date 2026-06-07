using Microsoft.EntityFrameworkCore;
using Sofragrancia_api.Domain.Entities;

namespace Sofragrancia_api.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Vendedor> Vendedores => Set<Vendedor>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();
    public DbSet<PedidoDeCompra> PedidosDeCompra => Set<PedidoDeCompra>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(e =>
        {
            e.ToTable("Cliente");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.TxRazaosocial).HasColumnName("tx_razaosocial").HasMaxLength(255);
            e.Property(x => x.TxNomefantasia).HasColumnName("tx_nomefantasia").HasMaxLength(255);
            e.Property(x => x.TxCnpj).HasColumnName("tx_cnpj").HasMaxLength(255);
            e.Property(x => x.TxTelefone).HasColumnName("tx_telefone").HasMaxLength(255);
            e.Property(x => x.TxEmail).HasColumnName("tx_email").HasMaxLength(255);
            e.Property(x => x.TxEndereco).HasColumnName("tx_endereco").HasMaxLength(255);
            e.Property(x => x.TxCidade).HasColumnName("tx_cidade").HasMaxLength(255);
            e.Property(x => x.TxEstado).HasColumnName("tx_estado").HasMaxLength(2);
            e.Property(x => x.DtCreatedate).HasColumnName("dt_createdate");
            e.Property(x => x.DtUpdatedate).HasColumnName("dt_updatedate");
            e.Property(x => x.FlIsenable).HasColumnName("isenable");
        });

        modelBuilder.Entity<Vendedor>(e =>
        {
            e.ToTable("Vendedor");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.TxNome).HasColumnName("tx_nome").HasMaxLength(255);
            e.Property(x => x.TxCpf).HasColumnName("tx_cpf").HasMaxLength(255);
            e.Property(x => x.TxTelefone).HasColumnName("tx_telefone").HasMaxLength(255);
            e.Property(x => x.TxEmail).HasColumnName("tx_email").HasMaxLength(255);
            e.Property(x => x.DtAdmissao).HasColumnName("dt_admissao");
            e.Property(x => x.DtCreatedate).HasColumnName("dt_createdate");
            e.Property(x => x.DtUpdatedate).HasColumnName("dt_updatedate");
            e.Property(x => x.FlIsenable).HasColumnName("fl_isenable");
        });

        modelBuilder.Entity<Fornecedor>(e =>
        {
            e.ToTable("Fornecedor");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.TxRazaosocial).HasColumnName("tx_razaosocial").HasMaxLength(255);
            e.Property(x => x.TxNomefantasia).HasColumnName("tx_nomefantasia").HasMaxLength(255);
            e.Property(x => x.TxCnpj).HasColumnName("tx_cnpj").HasMaxLength(255);
            e.Property(x => x.TxTelefone).HasColumnName("tx_telefone").HasMaxLength(255);
            e.Property(x => x.TxEmail).HasColumnName("tx_email").HasMaxLength(255);
            e.Property(x => x.TxEndereco).HasColumnName("tx_endereco").HasMaxLength(255);
            e.Property(x => x.TxCidade).HasColumnName("tx_cidade").HasMaxLength(255);
            e.Property(x => x.TxEstado).HasColumnName("tx_estado").HasMaxLength(255);
            e.Property(x => x.DtCreatedate).HasColumnName("dt_createdate");
            e.Property(x => x.DtUpdatedate).HasColumnName("dt_updatedate");
            e.Property(x => x.FlIsenable).HasColumnName("fl_isenable");
        });

        modelBuilder.Entity<Produto>(e =>
        {
            e.ToTable("Produto");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.TxDescricao).HasColumnName("tx_descrição").HasMaxLength(255);
            e.Property(x => x.TxUnidade).HasColumnName("tx_unidade").HasMaxLength(255);
            e.Property(x => x.NrPrecocusto).HasColumnName("nr_precocusto").HasColumnType("decimal(12,2)");
            e.Property(x => x.NrPrecovenda).HasColumnName("nr_precovenda").HasColumnType("decimal(12,2)");
            e.Property(x => x.NrEstoqueatual).HasColumnName("nr_estoqueatual");
            e.Property(x => x.NrEstoqueminimo).HasColumnName("nr_estoqueminimo");
            e.Property(x => x.DtCreatedate).HasColumnName("dt_createdate");
            e.Property(x => x.DtUpdatedate).HasColumnName("dt_updatedate");
            e.Property(x => x.FlIsenable).HasColumnName("fl_isenable");
        });

        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("Pedido");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.ClienteId).HasColumnName("Cliente_id");
            e.Property(x => x.VendedorId).HasColumnName("Vendedor_id");
            e.Property(x => x.NrPedido).HasColumnName("nr_pedido").HasMaxLength(255);
            e.Property(x => x.DtDatapedido).HasColumnName("dt_datapedido");
            e.Property(x => x.NrValorbruto).HasColumnName("nr_valorbruto").HasColumnType("decimal(12,2)");
            e.Property(x => x.NrValordesconto).HasColumnName("nr_valordesconto").HasColumnType("decimal(12,2)");
            e.Property(x => x.NrValorliquido).HasColumnName("nr_valorliquido").HasColumnType("decimal(12,2)");
            e.Property(x => x.DtCreatedate).HasColumnName("dt_createdate");
            e.Property(x => x.DtUpdatedate).HasColumnName("dt_updatedate");
            e.Property(x => x.FlIsenable).HasColumnName("fl_isenable");
            e.HasOne(x => x.Cliente).WithMany(c => c.Pedidos).HasForeignKey(x => x.ClienteId);
            e.HasOne(x => x.Vendedor).WithMany(v => v.Pedidos).HasForeignKey(x => x.VendedorId);
        });

        modelBuilder.Entity<ItemPedido>(e =>
        {
            e.ToTable("Item Pedido");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.PedidoId).HasColumnName("Pedido_id");
            e.Property(x => x.PedidoClienteId).HasColumnName("Pedido_Cliente_id");
            e.Property(x => x.PedidoVendedorId).HasColumnName("Pedido_Vendedor_id");
            e.Property(x => x.ProdutoId).HasColumnName("Produto_id");
            e.Property(x => x.NrQuantidade).HasColumnName("nr_quantidade");
            e.Property(x => x.NrPrecounitario).HasColumnName("nr_precounitario").HasColumnType("decimal(12,2)");
            e.Property(x => x.NrDescontounitario).HasColumnName("nr_descontounitario").HasColumnType("decimal(12,2)");
            e.Property(x => x.NrSubtotal).HasColumnName("nr_subtotal").HasColumnType("decimal(12,2)");
            e.Property(x => x.DtCreatedate).HasColumnName("dt_createdate");
            e.Property(x => x.DtUpdatedate).HasColumnName("dt_updatedate");
            e.Property(x => x.FlIsenable).HasColumnName("fl_isenable");
            e.HasOne(x => x.Pedido).WithMany(p => p.ItensPedido).HasForeignKey(x => x.PedidoId);
            e.HasOne(x => x.Produto).WithMany(p => p.ItensPedido).HasForeignKey(x => x.ProdutoId);
        });

        modelBuilder.Entity<PedidoDeCompra>(e =>
        {
            e.ToTable("Pedido de Compra");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.ProdutoId).HasColumnName("Produto_id");
            e.Property(x => x.FornecedorId).HasColumnName("Fornecedor_id");
            e.Property(x => x.NrQuantidade).HasColumnName("nr_quantidade");
            e.Property(x => x.NrPrecounitario).HasColumnName("nr_precounitario").HasMaxLength(255);
            e.Property(x => x.NrDescontounitario).HasColumnName("nr_descontounitario");
            e.Property(x => x.Subtotal).HasColumnName("subtotal");
            e.Property(x => x.DtCreatedate).HasColumnName("dt_createdate");
            e.Property(x => x.DtUpdatedate).HasColumnName("dt_updatedate");
            e.Property(x => x.FlIsenable).HasColumnName("fl_isenable");
            e.HasOne(x => x.Produto).WithMany(p => p.PedidosDeCompra).HasForeignKey(x => x.ProdutoId);
            e.HasOne(x => x.Fornecedor).WithMany(f => f.PedidosDeCompra).HasForeignKey(x => x.FornecedorId);
        });
    }
}
