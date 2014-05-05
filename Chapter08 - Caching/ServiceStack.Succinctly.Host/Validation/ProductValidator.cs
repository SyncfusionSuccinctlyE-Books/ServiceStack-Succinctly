using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;
using ServiceStack.Succinctly.ServiceInterface.ProductModel;

namespace ServiceStack.Succinctly.Host.Validation
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            string nameNotSpecifiedMsg = "Name has not been specified.";
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(nameNotSpecifiedMsg)
                .NotNull().WithMessage(nameNotSpecifiedMsg)
                .Length(1, 50).WithMessage(nameNotSpecifiedMsg);
        }
    }

    public class UpdateProductValidator : AbstractValidator<UpdateProduct>
    {
        public UpdateProductValidator()
        {
            string nameNotSpecifiedMsg = "Name has not been specified.";
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(nameNotSpecifiedMsg)
                .NotNull().WithMessage(nameNotSpecifiedMsg)
                .Length(1, 50).WithMessage(nameNotSpecifiedMsg);
        }
    }
}