using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderCreator.Application.Services;
using OrderCreator.Core.Abstractions;
using OrderCreator.DataAccess.Repositories;

namespace OrderCreator.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {

            services
                .AddPersistence(configuration)
                .AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOrdersService, OrdersService>();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services
                .AddDbContext<OrderCreatorDbContext>(opt => opt.UseNpgsql(connectionString))
                .AddScoped<IOrdersRepository, OrdersRepository>(); 

            return services;
        }
    }
}
