using System;
using System.Collections.Generic;

namespace ServiceStack.Succinctly.ServiceInterface.OrderModel
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsTakeAway { get; set; }
        public DateTime CreationDate { get; set; }
        public Status Status { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}