using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.WarehouseOrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IDBContainerRepository
    {
        Task CreateDatabaseIfNotExistsAsync();
        Task UploadContentAsync(DeliveryOrder warehouseOrder);
    }
}
