namespace OrderManagement.Core.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Version { get; set; }
    }
}