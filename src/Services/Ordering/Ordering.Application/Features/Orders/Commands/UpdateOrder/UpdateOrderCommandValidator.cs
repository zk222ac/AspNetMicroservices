using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            // Validation for UserName
            RuleFor(p => p.UserName)
                .NotNull().WithMessage("UserName is Required")
                .NotNull()
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters");

            // Validation for Email Address
            RuleFor(p => p.EmailAddress)
              .NotEmpty().WithMessage("EmailAddress is Required");

            // Validation for Total Price should not be less than 0

            RuleFor(p => p.TotalPrice)
              .NotEmpty().WithMessage("TotalPrices is Required")
              .GreaterThan(0).WithMessage("TotalPrices should be greater than zero");
        }
    }
}
