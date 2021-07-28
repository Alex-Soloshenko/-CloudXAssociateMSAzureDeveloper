using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IBlobContainerRepository
    {
        Task CreateContainerIfNotExistsAsync();
        Task UploadContentAsync(string content, string blobName);
    }
}
