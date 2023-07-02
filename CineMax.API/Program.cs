using CineMax.API.Extensions;
using CineMax.Application.Commands.CreateUser;
using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using CineMax.Core.Services.Auth;
using CineMax.Core.Services.Payment;
using CineMax.Infra.Auth;
using CineMax.Infra.Auth.Data;
using CineMax.Infra.Persistence;
using CineMax.Infra.Persistence.Repositories;
using CineMax.Infra.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Way2Commerce.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddVersioning();
builder.Services.AddSwagger();

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("CineMaxCsVini");
// ATENÇÃO! Alterar a referência da string de conexão
builder.Services.AddDbContext<CineMaxDbContext>(options => options
.UseSqlServer(connectionString));
builder.Services.AddDbContext<IdentityDataContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddMediatR(typeof(CreateUserCommand));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMovieRepository, MovieRepository>(); 
builder.Services.AddScoped<IClientRepository, ClientRepository>();  
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<ISectionSeatRepository, SectionSeatRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IPaymentRefundLogRepository, PaymentRefundLogRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();

//Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.UseCors(builder => builder
    .SetIsOriginAllowed(orign => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.MapControllers();

app.Run();
