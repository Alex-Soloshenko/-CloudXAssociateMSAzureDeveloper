using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DeliveryOrderProcessor
{
    public class DeliveryOrderProcessorFunction
    {
        private readonly ILogger<DeliveryOrderProcessorFunction> _logger;
        private readonly IDeliveryStorageService _deliveryStorageService;

        public DeliveryOrderProcessorFunction(ILogger<DeliveryOrderProcessorFunction> logger, IDeliveryStorageService deliveryStorageService)
        {
            _logger = logger;
            _deliveryStorageService = deliveryStorageService;
        }

        [Function("Process")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")]
            HttpRequestData req, FunctionContext executionContext)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var order = JsonConvert.DeserializeObject<Order>(requestBody);

            _logger.LogDebug($"Storing order: {order}");
            await _deliveryStorageService.StoreOrderAsync(order);
            _logger.LogDebug("Order is stored successfully");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            await response.WriteStringAsync($"Storing is successful! {order}");

            return response;
        }
    }
}
