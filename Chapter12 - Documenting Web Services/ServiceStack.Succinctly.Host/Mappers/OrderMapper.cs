using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceStack.Succinctly.ServiceInterface;
using ServiceStack.Text;
using SrvObj = ServiceStack.Succinctly.ServiceInterface.OrderModel;
using Domain = OrderManagement.Core.Domain;

namespace ServiceStack.Succinctly.Host.Mappers
{
    public class OrderMapper : IOrderMapper
    {
        static OrderMapper()
        {
            Mapper.CreateMap<Domain.Status, Status>();
            Mapper.CreateMap<Status, Domain.Status>();

            Mapper.CreateMap<SrvObj.Order, Domain.Order>();
            Mapper.CreateMap<SrvObj.OrderItem, Domain.OrderItem>();
            Mapper.CreateMap<SrvObj.Product, Domain.Product>();

            Mapper.CreateMap<Domain.Order, SrvObj.OrderResponse>();
            Mapper.CreateMap<Domain.OrderItem, SrvObj.OrderItemResponse>();
        }

        public Domain.Order ToOrder(SrvObj.Order request)
        {
            return Mapper.Map<Domain.Order>(request);
        }

        public SrvObj.OrderResponse ToOrderResponse(Domain.Order order)
        {
            var orderResponse = Mapper.Map<SrvObj.OrderResponse>(order);

            var orderSelfLink = "orders/{0}".Fmt(order.Id);

            orderResponse.Links.Add(SelfLink(orderSelfLink));
            orderResponse.Items.ForEach(x =>
                {
                    var productId = x.Product.Id;
                    var productLInk = "products/{0}".Fmt(productId);
                    var itemsLink = orderSelfLink + "/items/{0}".Fmt(x.Id);
                    x.Product.Links.Add(SelfLink(productLInk));
                    x.Links.Add(SelfLink(itemsLink));
                });
            return orderResponse;
        }

        private Link SelfLink(string uri)
        {
            return new Link
                {
                    Title = "self",
                    Rel = "self",
                    Href = uri
                };
        }

        private Link ParentLink(string uri)
        {
            return new Link
                {
                    Title = "parent",
                    Rel = "parent",
                    Href = uri
                };
        }

        public List<SrvObj.OrderResponse> ToOrderResponseList(List<Domain.Order> orders)
        {
            var orderResponseList = new List<SrvObj.OrderResponse>();
            orders.ForEach(x => orderResponseList.Add(ToOrderResponse(x)));
            return orderResponseList;
        }


        public SrvObj.OrderItemResponse ToOrderItemResponse(int orderId, Domain.OrderItem item)
        {
            var orderItemReponse = Mapper.Map<SrvObj.OrderItemResponse>(item);

            var productId = orderItemReponse.Product.Id;
            var orderLink = "orders/{0}".Fmt(orderId);
            var itemsLink = "/items/{0}".Fmt(item.Id);
            var productLink = "products/{0}".Fmt(productId);

            orderItemReponse.Links.Add(SelfLink(orderLink + itemsLink));
            orderItemReponse.Links.Add(ParentLink(orderLink));
            orderItemReponse.Product.Links.Add(SelfLink(productLink));

            return orderItemReponse;
        }

        public List<SrvObj.OrderItemResponse> ToOrderItemResponseList(int orderId,
                                                                        List<Domain.OrderItem> items)
        {
            var orderItemResponseList = new List<SrvObj.OrderItemResponse>();
            items.ForEach(x => orderItemResponseList.Add(ToOrderItemResponse(orderId, x)));
            return orderItemResponseList;
        }
    }
}