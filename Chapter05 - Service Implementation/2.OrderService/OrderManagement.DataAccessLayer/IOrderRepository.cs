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
}