using Application.Contexts.Users.Commands;
using FluentValidation;

namespace Application.Contexts.Users.Validators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUser>
    {
        public DeleteUserValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty()
                .WithMessage("Missing User ID");
        }
    }
}