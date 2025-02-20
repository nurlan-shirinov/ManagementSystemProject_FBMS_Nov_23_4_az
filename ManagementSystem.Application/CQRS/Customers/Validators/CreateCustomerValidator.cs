using FluentValidation;
using ManagementSystem.Application.CQRS.Customers.Commands.Requests;

namespace ManagementSystem.Application.CQRS.Customers.Validators;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(c => c.Email)
            .NotEmpty()
            .MaximumLength(255)
            .EmailAddress();
    }
}