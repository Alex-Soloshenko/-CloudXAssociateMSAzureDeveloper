﻿using System;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.WarehouseOrderAggregate
{
    public class WarehouseOrder : BaseEntity
    {
        private static readonly Random Random = new();
        private readonly int _id = Random.Next();

        public override int Id
        {
            get { return _id; }
            protected set { }
        }

        public Order Order { get; }

        public string Name
        {
            get
            {
                return $"{Id}:{Order.Id}";
            }
        }

        public WarehouseOrder(Order order)
        {
            Guard.Against.Null(order, nameof(order));

            Order = order;
        }
    }
}
