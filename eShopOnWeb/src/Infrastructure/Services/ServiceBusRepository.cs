using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    public class ServiceBusRepository : IServiceBusRepository
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public ServiceBusRepository(AzureWarehouseServiceBusSettings azureWarehouseServiceBusSettings)
        {
            _client = new ServiceBusClient(azureWarehouseServiceBusSettings.ConnectionString);
            _sender = _client.CreateSender(azureWarehouseServiceBusSettings.QueueName);
        }

        public async Task SendAsync(string content)
        {
            var serviceBusMessage = new ServiceBusMessage(content);
            await _sender.SendMessageAsync(serviceBusMessage);
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                await _client.DisposeAsync();
            }
            finally
            {
                await _sender.DisposeAsync();
            }

            GC.SuppressFinalize(this);
        }
    }
}
