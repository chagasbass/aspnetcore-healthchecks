using AspnetCore.Healthchecks.Data.DataContexts;
using AspnetCore.Healthchecks.Data.Repositories;
using AspnetCore.Healthchecks.Data.Services;
using AspnetCore.Healthchecks.Domain.Repositories;
using AspnetCore.Healthchecks.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetCore.Healthchecks.Configurations
{
    /// <summary>
    /// Extensão para a resolução de dependência da aplicação
    /// </summary>
    public static class DIConfigurationExtensions
    {
        public static IServiceCollection AddDIConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IAddressExternalService, AddressExternalService>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            return services;
        }

        public static IServiceCollection AddDataBaseConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var stringDeConexao = configuration.GetConnectionString("Database");

            services.AddDbContext<DataContext>(contexto =>
            {
                contexto
                .UseSqlServer(stringDeConexao);

            });

            return services;
        }
    }
}
