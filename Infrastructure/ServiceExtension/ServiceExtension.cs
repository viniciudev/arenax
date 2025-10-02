using Core;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContextClass>(options =>
            {
                //options.UseNpgsql("host=localhost;user id=postgres;password=123456789;database=Comercial3irmaos;Pooling=false;Timeout=300;CommandTimeout=300;");
                //options.UseNpgsql("host=89.117.146.50;user id=postgres;password=7A24Jdp1Rcyv;database=ComercialHomolog;Pooling=false;Timeout=300;CommandTimeout=300;");
                //options.UseNpgsql("host=localhost;user id=postgres;password=admin;database=4Axon;Pooling=false;Timeout=300;CommandTimeout=300;");
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b =>
                    {
                        b.UseNodaTime();
                        b.MigrationsAssembly("Infrastructure");
                    });

            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISportsCategoryRepository, SportsCategoryRepository>();
            services.AddScoped<ISportsCourtRepository, SportsCourtRepository>();
            services.AddScoped<ISportsCourtOperationRepository, SportsCourtOperationRepository>();
            services.AddScoped<ICourtEvaluationsRepository, CourtEvaluationsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISportsCourtAppointmentsRepository, SportsCourtAppointmentsRepository>();
            services.AddScoped<ISportsCenterRepository, SportsCenterRepository>();
            services.AddScoped<ISportsCenterUsersRepository, SportsCenterUsersRepository>();
            services.AddScoped<ISportsCourtCategoryRepository, SportsCourtCategoryRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ISportsCourtImageRepository, SportsCourtImageRepository>();
            services.AddScoped<ISportsRegistrationRepository, SportsRegistrationRepository>();
            services.AddScoped<INotificationsRepository, NotificationsRepository>();

            services.AddScoped<IImageUrlRepository, ImageUrlRepository>();
            services.AddScoped<IClientEvaluationRepository, ClientEvaluationRepository>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            return services;
        }
    }
}
