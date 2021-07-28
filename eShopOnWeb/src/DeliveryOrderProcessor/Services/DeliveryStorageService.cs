using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.WarehouseOrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace DeliveryOrderProcessor.Services
{
    internal class DeliveryStorageService : IDeliveryStorageService
    {
        private readonly IDBContainerRepository _dbContainerRepository;

        public DeliveryStorageService(IDBContainerRepository dbContainerRepository)
        {
            _dbContainerRepository = dbContainerRepository;
        }

        public async Task StoreOrderAsync(Order order)
        {
            var deliveryOrder = new DeliveryOrder(order);
            await _dbContainerRepository.CreateDatabaseIfNotExistsAsync();
            await _dbContainerRepository.UploadContentAsync(deliveryOrder);
        }
    }
}