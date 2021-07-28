using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderItemReserver
{
    public class OrderItemReserverFunction
    {
        private readonly ILogger<OrderItemReserverFunction> _logger;
        private readonly IWarehouseReservationService _warehouseReservationService;

        public OrderItemReserverFunction(ILogger<OrderItemReserverFunction> logger, IWarehouseReservationService warehouseReservationService)
        {
            _logger = logger;
            _warehouseReservationService = warehouseReservationService;
        }

        [Function("Reserve")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")]
            HttpRequestData req, FunctionContext executionContext)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var order = JsonConvert.DeserializeObject<Order>(requestBody);

            _logger.LogDebug($"Reserving order: {order}");
            await _warehouseReservationService.ReserveOrderAsync(order);
            _logger.LogDebug("Reservation is completed successfully");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            await response.WriteStringAsync($"Reservation is successful! {order}");

            return response;
        }
    }
}
