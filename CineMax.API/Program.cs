using CineMax.Application.Commands.CreateUser;
using CineMax.Infra.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString1 = builder.Configuration.GetConnectionString("CineMaxCsVini");
// ATENÇÃO! Alterar a referência da string de conexão
builder.Services.AddDbContext<CineMaxDbContext>(options => options
.UseSqlServer(connectionString1));

//MediatR utiliza o assembly para mapear todos os outros com a interfarce iRequest e iRequestHandler
builder.Services.AddMediatR(typeof(CreateUserCommand));


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
