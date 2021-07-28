using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderItemReserver.Services;

namespace OrderItemReserver.Configuration
{
    internal static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            var azureWarehouseStorageSection = configuration.GetSection(nameof(AzureWarehouseStorageSettings));
            var azureWarehouseStorageSettings = azureWarehouseStorageSection.Get<AzureWarehouseStorageSettings>();

            services.AddScoped<IBlobContainerRepository>(_ => new BlobContainerRepository(azureWarehouseStorageSettings));
            services.AddScoped<IWarehouseReservationService, WarehouseReservationService>();

            return services;
        }
    }
}
