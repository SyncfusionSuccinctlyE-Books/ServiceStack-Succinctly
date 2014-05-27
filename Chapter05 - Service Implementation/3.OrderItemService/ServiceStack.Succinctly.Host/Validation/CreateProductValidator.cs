using ServiceStack.FluentValidation;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Validation
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            string nameNotSpecifiedMsg = "Name has not been specified.";
            string maxLenghtMsg = "Name cannot be longer than 50 characters.";

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(nameNotSpecifiedMsg)
                .NotNull().WithMessage(nameNotSpecifiedMsg)
                .Length(1, 50).WithMessage(maxLenghtMsg);
        }
    }
}