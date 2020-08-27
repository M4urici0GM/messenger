using Application.Contexts.Users.Commands;
using FluentValidation;

namespace Application.Contexts.Users.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage("First name is required");
            RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage("Last name is required");
            RuleFor(p => p.Password)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,30}$")
                .WithMessage(
                    @"Password must have at least 6 and maximum of 30 characters, at least one number and one special character");
            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email address");
        }
    }
}