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

// Localizações
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<LocalizacaoService>();

// Tipo Patrimônio
builder.Services.AddScoped<ITipoPatrimonioRepository, TipoPatrimonioRepository>();
builder.Services.AddScoped<TipoPatrimonioService>();

// Tipo Usuário
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
