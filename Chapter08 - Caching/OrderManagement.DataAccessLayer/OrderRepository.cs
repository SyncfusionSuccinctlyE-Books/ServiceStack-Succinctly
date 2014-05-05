using System;
using System.Collections.Generic;
using System.Linq;
using OrderManagement.Core.Domain;

namespace OrderManagement.DataAccessLayer
{
    public interface IOrderRepository
    {
        Order GetById(int id); //returns an order given the order id
        Order Add(Order order); //adds a new order in the repository
        void Delete(List<Order> orders); //deletes a list of orders
        void Delete(int orderId); //deletes an order given the order id     
        void Delete(); //deletes all orders  
        Order Update(Order order); //updates an existing order.
        List<Order> GetAllOrders(); //returns a list of all availalbe orders
    }

    /// <summary>
    /// In-memory orders repository, that fakes the database
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private static readonly List<Order> Orders = new List<Order>();       
        private ProductRepository Products = new ProductRepository();

        public OrderRepository()
        {
            Init();
        }

        public void Init()
        {
            if (!Orders.Any())
            {
                //Create three orders.
                Orders.Add(GetExampleOrder(1, true, 6, 2));
                Orders.Add(GetExampleOrder(2, false, 3, 4));
                Orders.Add(GetExampleOrder(3, true, 1, 5));
            }
        }

        private Order GetExampleOrder(int id, bool isTakeAway, int product1, int product2)
        {
            var order = new Order
                {
                    Id = id,
                    IsTakeAway = isTakeAway,
                    CreationDate = DateTime.Now,
                    Version = 1,
                    Status = new Status { Id = 1, Name = "Active"}
                };

            order.Items = new List<OrderItem>();

            //adding two items by default.
            order.Items.Add(new OrderItem()
                {
                    Id = product1,
                    Product = Products.GetById(product1),
                    Quantity = 1,
                    Version = 1
                });
            order.Items.Add(new OrderItem()
                {
                    Id = product2,
                    Product = Products.GetById(product2),
                    Quantity = 1,
                    Version = 1
                });
            return order;
        }

        public Order Add(Order order)
        {
            var nextOrderId = GetNextId();
            order.Id = nextOrderId;

            order.Items.ForEach(x => x.Id = GetNextItemId());
            Orders.Add(order);
            return order;
        }

        private int GetNextId()
        {
            int max = Orders.Max(x => x.Id);
            return max + 1;
        }

        private int GetNextItemId()
        {
            int max = Orders.Max(x => x.Items.Max(y => y.Id));
            return max + 1;
        }

        public void Delete(List<Order> orders)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                Orders.Remove(orders[i]);
            }
        }

        public void Delete(int orderId)
        {
            Order order = Orders.FirstOrDefault(x => x.Id == orderId);
            Orders.Remove(order);
        }

        public Order GetById(int id)
        {
            return Orders.FirstOrDefault(x => x.Id == id);
        }

        public List<Order> GetAllOrders()
        {
            return Orders.Select(x => x).ToList();
        }

        public Order Update(Order order)
        {
            Order o = GetById(order.Id);
            o.IsTakeAway = order.IsTakeAway;
            o.CreationDate = order.CreationDate;
            o.Status = order.Status;

            o.Items.Clear();
            order.Items.ForEach(x => o.Items.Add(x));
            return order;
        }


        public void Delete()
        {
            Orders.Clear();
        }
    }
}