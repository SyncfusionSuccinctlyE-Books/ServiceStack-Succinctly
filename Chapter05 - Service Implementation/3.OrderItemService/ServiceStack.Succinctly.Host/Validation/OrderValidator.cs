using System;
using ServiceStack.FluentValidation;
using ServiceStack.Succinctly.ServiceInterface.OrderModel;

namespace ServiceStack.Succinctly.Host.Validation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(r => r.CreationDate)
                .LessThan(DateTime.Now.AddSeconds(10))
                .WithMessage("Creation Data shouldn't be in the future");

            RuleFor(r => r.Items)
                .NotEmpty()
                .WithMessage("Order Items should be specified");
        }
    }
}