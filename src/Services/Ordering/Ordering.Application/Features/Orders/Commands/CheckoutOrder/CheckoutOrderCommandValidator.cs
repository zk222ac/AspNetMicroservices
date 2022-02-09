using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
   public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
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
