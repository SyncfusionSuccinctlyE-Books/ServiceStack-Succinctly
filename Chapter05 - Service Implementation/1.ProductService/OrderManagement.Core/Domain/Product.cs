namespace OrderManagement.Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public Status Status { get; set; }
    }
}