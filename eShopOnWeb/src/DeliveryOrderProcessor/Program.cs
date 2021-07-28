using DeliveryOrderProcessor.Configuration;
using Microsoft.Extensions.Hosting;

namespace DeliveryOrderProcessor
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices((context, services) => { services.AddCoreServices(context.Configuration); })
                .Build();

            host.Run();
        }
    }
}