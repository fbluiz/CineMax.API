using CineMax.Application.Commands.CreateUser;
using CineMax.Core.Repositories;
using CineMax.Infra.Persistence;
using CineMax.Infra.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var connectionString = builder.Configuration.GetConnectionString("CineMaxCsFb");
// ATENÇÃO! Alterar a referência da string de conexão
builder.Services.AddDbContext<ICineMaxDbContext>(options => options
.UseSqlServer(connectionString));

//MediatR utiliza o assembly para mapear todos os outros com a interfarce iRequest e iRequestHandler
builder.Services.AddMediatR(typeof(CreateUserCommand));

//Mapeamento padrão repository
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

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
