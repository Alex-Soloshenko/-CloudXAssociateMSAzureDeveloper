using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public abstract class BaseCosmosEntity
    {
        public virtual string Id { get; protected set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
    }
}