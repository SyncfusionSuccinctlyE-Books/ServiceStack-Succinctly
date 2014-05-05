using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using OrderManagement.DataAccessLayer;
using ServiceStack.CacheAccess;
using ServiceStack.Messaging;
using ServiceStack.Redis;
using ServiceStack.ServiceHost;
using ServiceStack.Succinctly.Host.Mappers;
using ServiceStack.Succinctly.ServiceInterface;
using ServiceStack.Succinctly.ServiceInterface.OrderItemModel;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using Domain = OrderManagement.Core.Domain;

namespace ServiceStack.Succinctly.Host.Services
{
    public interface IOrderItemService
    {
        IOrderRepository OrderRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        IStatusRepository StatusRepository { get; set; }
        IOrderMapper OrderMapper { get; set; }

        OrderItemResponse Get(GetOrderItem request);
        OrderItemsResponse Get(GetOrderItems request);
    }

    public class OrderItemService : ServiceStack.ServiceInterface.Service, IOrderItemService
    {
        public IOrderRepository OrderRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }
        public IOrderMapper OrderMapper { get; set; }

        public OrderItemResponse Get(GetOrderItem request)
        {
            var order = OrderRepository.GetById(request.OrderId);

            Domain.OrderItem orderItem =
                order
                    .Items
                    .FirstOrDefault(x => x.Id == request.ItemId);

            if (orderItem == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            OrderItemResponse response =
                OrderMapper
                .ToOrderItemResponse(order.Id, orderItem);

            return response;
        }

        public OrderItemsResponse Get(GetOrderItems request)
        {
            var order = OrderRepository.GetById(request.OrderId);

            List<Domain.OrderItem> orderItems = order.Items;

            if (orderItems == null || orderItems.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
            else
            {
                return new OrderItemsResponse
                    {
                        Items = OrderMapper
                        .ToOrderItemResponseList(request.OrderId, orderItems)
                    };
            }
        }
    }
}