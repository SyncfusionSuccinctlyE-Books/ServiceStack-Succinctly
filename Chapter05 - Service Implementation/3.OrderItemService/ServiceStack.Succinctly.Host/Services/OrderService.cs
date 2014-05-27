using System.Net;
using OrderManagement.DataAccessLayer;
using ServiceStack.Common.Web;
using ServiceStack.Succinctly.Host.Mappers;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;

namespace ServiceStack.Succinctly.Host.Services
{
    public class OrderService : ServiceStack.ServiceInterface.Service
    {
        public IOrderRepository OrderRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }
        public IOrderMapper OrderMapper { get; set; }


        //returns a single order
        public OrderResponse Get(GetOrder request)
        {
            var domainObject = OrderRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
            else
            {
                //transform to OrderResponse and return
                return OrderMapper.ToOrderResponse(domainObject);
            }
        }

        //returns all the orders
        public OrdersResponse Get(GetOrders request)
        {
            //get data from the database
            var orders = OrderRepository.GetAllOrders();

            //transform to OrdersResponse and return.
            return new OrdersResponse()
            {
                Orders = OrderMapper.ToOrderResponseList(orders)
            };
        }

        public OrderResponse Post(Order request)
        {
            //transforming the ID's into real object objects.
            var newOrder = OrderMapper.ToOrder(request);
            newOrder.Status = StatusRepository.GetById(request.Status.Id);

            newOrder.Items.ForEach(x =>
            {
                x.Product = ProductRepository.GetById(x.Product.Id);
            });

            //storing data to databas.
            newOrder = OrderRepository.Add(newOrder);

            //transform to OrderResponse
            var response = OrderMapper.ToOrderResponse(newOrder);

            //manipulate the header and StatusCode.
            Response.AddHeader("Location", Request.AbsoluteUri + "/" + newOrder.Id);
            Response.StatusCode = (int)HttpStatusCode.Created;

            return response;
        }

        //updates an existing order /orders/{id}
        public OrderResponse Put(Order request)
        {
            var domainObject = OrderRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            var updatedOrder = OrderMapper.ToOrder(request);
            updatedOrder.Status = StatusRepository.GetById(request.Status.Id);
            updatedOrder.Items.ForEach(x =>
            {
                x.Product = ProductRepository.GetById(x.Product.Id);
            });

            //Store data to Database
            var order = OrderRepository.Update(updatedOrder);

            //transform to OrderResponse and return 
            return OrderMapper.ToOrderResponse(order);
        }

        public HttpResult Delete(DeleteOrder request)
        {
            var domainObject = OrderRepository.GetById(request.Id);
            if (domainObject == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                //delete order in the database 
                OrderRepository.Delete(request.Id);
                Response.StatusCode = (int)HttpStatusCode.NoContent;
            }

            //not returning any body!
            return null;
        }

    }
}