using CineMax.Application.Commands.CreateUser;
using CineMax.Core.Auth;
using CineMax.Core.Interfaces;
using CineMax.Core.Repositories;
using CineMax.Core.Services;
using CineMax.Infra.Auth.Data;
using CineMax.Infra.Persistence;
using CineMax.Infra.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CineMax.API.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CineMaxDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CineMaxCsFb"))
            );

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CineMaxCsFb"))
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

           services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISectionSeatRepository, SectionSeatRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IPaymentRefundLogRepository, PaymentRefundLogRepository>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddMediatR(typeof(CreateUserCommand));

        }
    }
}