using System.Net;
using System.Runtime.Serialization;
using System.Web;
using ServiceStack.ServiceHost;

namespace ServiceStack.Succinctly.ServiceInterface.ProductModel
{

[DataContract]
[Route("/products/{Id}",
    Verbs="GET",
    Notes = "Gets the product by id",
    Summary = "Object that doesn't need to be created directly")]
[Api("Get the product by id")]
[ApiResponse(HttpStatusCode.NotFound, "No products have been found in the repository")]
public class GetProduct
{
    [DataMember]
    [ApiMember(AllowMultiple = false,
        DataType = "int",
        Description = "Represents the ID passed in the URI",
        IsRequired = false,
        Name = "Id",
        ParameterType = "int",
        Verb = "GET")]
    [ApiAllowableValues("family", typeof(int))] //Enum
    public int Id { get; set; }
}
}