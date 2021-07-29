using System;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.WarehouseOrderAggregate
{
    public class DeliveryOrder : BaseCosmosEntity
    {
        private static readonly Random Random = new();
        private readonly int _id = Random.Next();

        public override string Id => _id.ToString();

        public override string PartitionKey
        {
            get => Id;
            set { }
        }


        public Order Order { get; }

        public DeliveryOrder(Order order)
        {
            Guard.Against.Null(order, nameof(order));

            Order = order;
        }
    }
}