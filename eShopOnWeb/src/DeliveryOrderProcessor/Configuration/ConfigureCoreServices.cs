using DeliveryOrderProcessor.Services;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryOrderProcessor.Configuration
{
    internal static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            var azureDeliveryStorageSettingsSection = configuration.GetSection(nameof(AzureDeliveryStorageSettings));
            var azureDeliveryStorageSettings = azureDeliveryStorageSettingsSection.Get<AzureDeliveryStorageSettings>();

            services.AddScoped<IDBContainerRepository>(_ => new DBContainerRepository(azureDeliveryStorageSettings));
            services.AddScoped<IDeliveryStorageService, DeliveryStorageService>();

            return services;
        }
    }
}
