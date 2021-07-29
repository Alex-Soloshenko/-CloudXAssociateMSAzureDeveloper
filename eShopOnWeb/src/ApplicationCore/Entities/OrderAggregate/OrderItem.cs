using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public CatalogItemOrdered ItemOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Units { get; private set; }

        private OrderItem()
        {
            // required by EF
        }

        public OrderItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }

        [JsonConstructor]
        public OrderItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units, int id)
            : this(itemOrdered, unitPrice, units)
        {
            Id = id;
        }
    }
}
