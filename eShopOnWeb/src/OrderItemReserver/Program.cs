using Microsoft.Extensions.Hosting;
using OrderItemReserver.Configuration;

namespace OrderItemReserver
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