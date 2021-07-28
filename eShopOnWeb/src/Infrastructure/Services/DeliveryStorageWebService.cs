using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    public class DeliveryStorageWebService : IDeliveryStorageService
    {
        private const string HttpMethodName = "Process";
        private readonly HttpClient _httpClient;

        public DeliveryStorageWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task StoreOrderAsync(Order order)
        {
            var serializedWarehouseOrder = JsonConvert.SerializeObject(order);
            var data = new StringContent(serializedWarehouseOrder, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(HttpMethodName, data);
            if (!response.IsSuccessStatusCode)
            {
                throw new MethodAccessException(response.ToString());
            }
        }
    }
}
