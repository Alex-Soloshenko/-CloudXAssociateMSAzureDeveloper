using System;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IServiceBusRepository : IAsyncDisposable
    {
        Task SendAsync(string content);
    }
}
