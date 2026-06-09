using Microsoft.EntityFrameworkCore;
using Sofragrancia_api.Application.DTOs;
using Sofragrancia_api.Application.Interfaces;
using Sofragrancia_api.Application.Services;
using Sofragrancia_api.Domain.Interfaces;
using Sofragrancia_api.Infrastructure.Data;
using Sofragrancia_api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Sofragrancia API", Version = "v1" });
});

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Repositories
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
builder.Services.AddScoped<IReposicaoRepository, ReposicaoRepository>();

// Services
builder.Services.AddScoped<IReposicaoBusinessRule, ReposicaoBusinessRule>();
builder.Services.AddScoped<IItemPedidoBusinessRule, ItemPedidoBusinessRule>();
builder.Services.AddScoped<IService<ClienteDto, ClienteCreateDto>, ClienteService>();
builder.Services.AddScoped<IService<VendedorDto, VendedorCreateDto>, VendedorService>();
builder.Services.AddScoped<IService<FornecedorDto, FornecedorCreateDto>, FornecedorService>();
builder.Services.AddScoped<IService<ProdutoDto, ProdutoCreateDto>, ProdutoService>();
builder.Services.AddScoped<IService<PedidoDto, PedidoCreateDto>, PedidoService>();
builder.Services.AddScoped<IService<ItemPedidoDto, ItemPedidoCreateDto>, ItemPedidoService>();
builder.Services.AddScoped<IService<ReposicaoDto, ReposicaoCreateDto>, ReposicaoService>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sofragrancia API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
