using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.WarehouseOrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Newtonsoft.Json;

namespace OrderItemReserver.Services
{
    internal class WarehouseReservationService : IWarehouseReservationService
    {
        private readonly IBlobContainerRepository _blobContainerRepository;

        public WarehouseReservationService(IBlobContainerRepository blobContainerRepository)
        {
            _blobContainerRepository = blobContainerRepository;
        }

        public async Task ReserveOrderAsync(Order order)
        {
            var warehouseOrder = new WarehouseOrder(order);
            var serializedWarehouseOrder = JsonConvert.SerializeObject(warehouseOrder);
            await _blobContainerRepository.CreateContainerIfNotExistsAsync();
            await _blobContainerRepository.UploadContentAsync(serializedWarehouseOrder, warehouseOrder.Name);
        }
    }
}
