using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace OrderItemReserver.Services
{
    internal class BlobContainerRepository : IBlobContainerRepository
    {
        private readonly BlobContainerClient _client;

        public BlobContainerRepository(AzureWarehouseStorageSettings azureWarehouseStorageSettings)
        {
            _client = new BlobContainerClient(
                azureWarehouseStorageSettings.ConnectionString,
                azureWarehouseStorageSettings.ContainerName
            );
        }

        public async Task CreateContainerIfNotExistsAsync()
        {
            await _client.CreateIfNotExistsAsync();
        }

        public async Task UploadContentAsync(string content, string blobName)
        {
            var blob = _client.GetBlobClient($"{blobName}.json");

            var binaryData = BinaryData.FromString(content);
            await blob.UploadAsync(binaryData, true);
        }
    }
}
