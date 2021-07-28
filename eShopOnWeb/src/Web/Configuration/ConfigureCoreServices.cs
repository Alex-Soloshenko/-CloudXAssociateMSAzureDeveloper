using System;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Logging;
using Microsoft.eShopWeb.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.eShopWeb.Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailSender, EmailSender>();

            var section = configuration.GetSection(nameof(AzureWarehouseServiceBusSettings));
            var serviceBusSettings = section.Get<AzureWarehouseServiceBusSettings>();

            services.AddSingleton<IServiceBusRepository>(_ => new ServiceBusRepository(serviceBusSettings));
            services.AddSingleton<IWarehouseReservationService, WarehouseReservationWebService>();

            var settings = configuration.Get<DeliveryStorageSettings>();
            var secret = configuration.Get<DeliveryStorageSecretSettings>();
            services.AddHttpClient<IDeliveryStorageService, DeliveryStorageWebService>(client =>
            {
                client.BaseAddress = new Uri(settings.DeliveryFunctionBaseUrl);
                client.DefaultRequestHeaders.Add("x-functions-key", secret.FunctionKey);
            });

            return services;
        }
    }
}
