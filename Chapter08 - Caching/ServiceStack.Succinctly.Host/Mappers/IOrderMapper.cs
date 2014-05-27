using System.Collections.Generic;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;
using Order = OrderManagement.Core.Domain.Order;
using OrderItem = OrderManagement.Core.Domain.OrderItem;

namespace ServiceStack.Succinctly.Host.Mappers
{
    public interface IOrderMapper
    {
        OrderManagement.Core.Domain.Order ToOrder(ServiceInterface.OrderModel.Order request);
        ServiceInterface.OrderModel.OrderResponse ToOrderResponse(OrderManagement.Core.Domain.Order order);
        List<OrderResponse> ToOrderResponseList(List<Order> orders);
        ServiceInterface.OrderModel.OrderItemResponse ToOrderItemResponse(int orderId, OrderManagement.Core.Domain.OrderItem orderItem);
        List<OrderItemResponse> ToOrderItemResponseList(int orderId, List<OrderItem> items);
    }
}