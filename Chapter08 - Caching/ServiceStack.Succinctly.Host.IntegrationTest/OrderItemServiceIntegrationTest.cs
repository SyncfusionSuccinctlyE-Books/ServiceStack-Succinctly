using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Succinctly.ServiceInterface.OrderItemModel;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;

namespace ServiceStack.Succinctly.Host.IntegrationTest
{
    [TestClass]
    public class OrderItemServiceIntegrationTest
    {
        [TestMethod]
        public void GetOrderItemsByOrderId()
        {
            //ARRANGE --- 
            int ORDER_ID = 1;
            string ITEMS_LINK = "/orders/" + ORDER_ID + "/items";

            var client = new XmlServiceClient("http://localhost:50712/");

            //ACT  ------ 
            var items = client.Get<OrderItemsResponse>(ITEMS_LINK);

            //ASSERT ---- 
            Assert.IsNotNull(items != null);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count == 2);
        }

        [TestMethod]
        public void GetOrderItemByOrderId()
        {
            //ARRANGE --- 
            var ITEM_ID = "2";            
            var client = new XmlServiceClient("http://localhost:50712/");
            string ITEM_LINK = "/orders/1/items/2";

            //ACT  ------ 
            var item = client.Get<OrderItemResponse>(ITEM_LINK);

            //ASSERT ---- 
            Assert.IsNotNull(item != null);
            Assert.IsTrue(item.Id.ToString() == ITEM_ID);
        }
    }
}