using FluentValidation;
using Messenger.Application.EntitiesContext.UserContext.Commands;

namespace Messenger.Application.EntitiesContext.UserContext.Validators
{
    public class AuthenticateValidator : AbstractValidator<Authenticate>
    {
        public AuthenticateValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Missing username");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Missing password");
        }
    }
}