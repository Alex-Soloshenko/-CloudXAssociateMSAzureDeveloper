using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.WarehouseOrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace DeliveryOrderProcessor.Services
{
    internal class DBContainerRepository : IDBContainerRepository
    {
        private readonly AzureDeliveryStorageSettings _azureDeliveryStorageSettings;
        private readonly CosmosClient _cosmosClient;

        private Container _container;
        private Database _database;

        public DBContainerRepository(AzureDeliveryStorageSettings azureDeliveryStorageSettings)
        {
            _azureDeliveryStorageSettings = azureDeliveryStorageSettings;
            _cosmosClient = InstantiateCosmosClient();
        }

        public async Task CreateDatabaseIfNotExistsAsync()
        {
            if (_container != null)
                return;

            _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_azureDeliveryStorageSettings.DatabaseName);
            _container = await _database.CreateContainerIfNotExistsAsync(_azureDeliveryStorageSettings.ContainerName, "/partitionKey");
        }

        public async Task UploadContentAsync(DeliveryOrder warehouseOrder)
        {
            await _container.CreateItemAsync(warehouseOrder);
        }


        private CosmosClient InstantiateCosmosClient()
        {
            return new(_azureDeliveryStorageSettings.ConnectionString,
                new CosmosClientOptions
                {
                    SerializerOptions = new CosmosSerializationOptions
                        {PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase}
                });
        }
    }
}
