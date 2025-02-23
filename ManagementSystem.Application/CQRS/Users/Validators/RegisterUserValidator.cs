using FluentValidation;
using static ManagementSystem.Application.CQRS.Users.Handlers.Commands.Register;

namespace ManagementSystem.Application.CQRS.Users.Validators;

public class RegisterUserValidator : AbstractValidator<Command>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Name cannot be emoty null or whitespace")
            .MaximumLength(255);

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email should be in correct format")
            .MaximumLength(70).WithMessage("Maximum 70 caracter is required");

        RuleFor(u => u.Phone)
            .NotEmpty().WithMessage("Phone cannot be empty")
            .MaximumLength(50).WithMessage("Maximum length is 50")
            .Matches(@"^\+994(5[015]|7[07])\d{7}$").WithMessage("Mobile phone format is +994");

        RuleFor(u => u.Surname)
            .NotEmpty()
            .MaximumLength(50);

    }
}
