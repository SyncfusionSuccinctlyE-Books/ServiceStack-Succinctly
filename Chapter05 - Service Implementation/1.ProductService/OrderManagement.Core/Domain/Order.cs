using System;
using System.Collections.Generic;

namespace OrderManagement.Core.Domain
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public int Id { get; set; }
        public bool IsTakeAway { get; set; }
        public DateTime CreationDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
    }
}