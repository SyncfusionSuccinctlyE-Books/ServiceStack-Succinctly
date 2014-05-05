namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{
    public class UpdateProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}