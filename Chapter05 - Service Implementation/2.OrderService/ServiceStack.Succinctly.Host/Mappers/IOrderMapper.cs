using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SrvObj = ServiceStack.Succinctly.ServiceInterface.OrderModel;
using Domain = OrderManagement.Core.Domain;

namespace ServiceStack.Succinctly.Host.Mappers
{
    public interface IOrderMapper
    {
        Domain.Order ToOrder(SrvObj.Order request);
        SrvObj.OrderResponse ToOrderResponse(Domain.Order order);
        List<SrvObj.OrderResponse> ToOrderResponseList(List<Domain.Order> orders);

        SrvObj.OrderItemResponse ToOrderItemResponse
            (int orderId, Domain.OrderItem orderItem);

        List<SrvObj.OrderItemResponse> ToOrderItemResponseList
            (int orderId, List<Domain.OrderItem> items);
    }
}