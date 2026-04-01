using DotNetEnv;
using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Interfaces;
using GestaoPatrimonios.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Carregando o .env.
Env.Load();

// Pegando a connection string.
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;

// Conexão com banco.
builder.Services.AddDbContext<GestaoPatrimoniosContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Áreas
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<AreaService>();

// Bairros
builder.Services.AddScoped<IBairroRepository, BairroRepository>();
builder.Services.AddScoped<BairroService>();

// Cargos
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<CargoService>();

// Cidades
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();

// Endereços
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<EnderecoService>();

// Localizações
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<LocalizacaoService>();

// Status Patrimônios
builder.Services.AddScoped<IStatusPatrimonioRepository, StatusPatrimonioRepository>();
builder.Services.AddScoped<StatusPatrimonioService>();

// Status Transferências
builder.Services.AddScoped<IStatusTransferenciaRepository, StatusTransferenciaRepository>();
builder.Services.AddScoped<StatusTransferenciaService>();

// Tipos Alterações
builder.Services.AddScoped<ITipoAlteracaoRepository, TipoAlteracaoRepository>();
builder.Services.AddScoped<TipoAlteracaoService>();

// Tipos Patrimônios
builder.Services.AddScoped<ITipoPatrimonioRepository, TipoPatrimonioRepository>();
builder.Services.AddScoped<TipoPatrimonioService>();

// Tipos Usuários
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<TipoUsuarioService>();

// Usuários
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
