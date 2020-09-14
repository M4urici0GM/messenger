using FluentValidation;
using Messenger.Application.EntitiesContext.UserContext.Commands;

namespace Messenger.Application.EntitiesContext.UserContext.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(x => x.Username)
                .NotEmpty()
                .Matches(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,29}$")
                .WithMessage("Invalid username");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Missing first name");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Missing last name");

            RuleFor(x => x.Password)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,30}$")
                .WithMessage(
                    @"Password must have at least 6 and maximum of 30 characters, at least one number and one special character");
        }
    }
}