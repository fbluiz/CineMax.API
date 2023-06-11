using CineMax.API.Extensions;
using CineMax.Application.Commands.CreateUser;
using CineMax.Core.Repositories;
using CineMax.Core.Services.Auth;
using CineMax.Infra.Auth;
using CineMax.Infra.Auth.Data;
using CineMax.Infra.Persistence;
using CineMax.Infra.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    // Configurar o Swagger
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CineMax.API", Version = "v1" });

    // Adicionar suporte para autenticação com token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("CineMaxCsFb");
// ATENÇÃO! Alterar a referência da string de conexão
builder.Services.AddDbContext<ICineMaxDbContext>(options => options
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

app.MapControllers();

app.Run();
