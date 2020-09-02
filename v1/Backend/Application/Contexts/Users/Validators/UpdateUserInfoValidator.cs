using Application.Contexts.Users.Commands;
using FluentValidation;

namespace Application.Contexts.Users.Validators
{
    public class UpdateUserInfoValidator : AbstractValidator<UpdateUserInfo>
    {
        public UpdateUserInfoValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.NewPassword)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,30}$")
                .WithMessage(@"
                    New password must have at least 6 and maximum of 30 characters, 
                    at least one number and one special character")
                .When(p => !string.IsNullOrEmpty(p.CurrentPassword));;
            RuleFor(p => p.CurrentPassword)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,30}$")
                .WithMessage(@"Current password needed when updating password")
                .When(p => !string.IsNullOrEmpty(p.NewPassword));;
        }
    }
}