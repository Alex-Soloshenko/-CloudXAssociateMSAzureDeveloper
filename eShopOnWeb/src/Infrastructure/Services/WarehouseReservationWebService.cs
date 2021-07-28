using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    public class WarehouseReservationWebService : IWarehouseReservationService
    {
        private readonly IServiceBusRepository _serviceBusRepository;

        public WarehouseReservationWebService(IServiceBusRepository serviceBusRepository)
        {
            _serviceBusRepository = serviceBusRepository;
        }

        public async Task ReserveOrderAsync(Order order)
        {
            var serializedWarehouseOrder = JsonConvert.SerializeObject(order);
            await _serviceBusRepository.SendAsync(serializedWarehouseOrder);
        }
    }
}
