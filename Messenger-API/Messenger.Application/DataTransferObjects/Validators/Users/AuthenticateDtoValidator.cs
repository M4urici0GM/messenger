using FluentValidation;
using Messenger.Application.DataTransferObjects.Users;

namespace Messenger.Application.DataTransferObjects.Validators.Users
{
    public class AuthenticateDtoValidator : AbstractValidator<AuthenticateDto>
    {
        public AuthenticateDtoValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .When(x => x.GrantType == "password")
                .WithMessage("Missing Password");

            RuleFor(x => x.Username)
                .NotEmpty()
                .When(x => x.GrantType == "password")
                .WithMessage("Missing username");

            RuleFor(x => x.Token)
                .NotEmpty()
                .When(x => x.GrantType == "refreshToken")
                .WithMessage("Missing refresh token");

            RuleFor(x => x.GrantType)
                .Must(s => !string.IsNullOrEmpty(s) && (s.Equals("password") || s.Equals("refreshToken")))
                .WithMessage("Invalid Grant-Type");
        }
    }
}