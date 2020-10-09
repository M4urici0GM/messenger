using System;
using FluentValidation;
using Messenger.Application.EntitiesContext.MessageContext.Commands;

namespace Messenger.Application.EntitiesContext.MessageContext.Validators
{
    public class CreateMessageValidator : AbstractValidator<CreateMessage>
    {
        public CreateMessageValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(1024)
                .WithMessage("Missing Message Content.");

            RuleFor(x => x.ContentType)
                .NotEmpty()
                .WithMessage("Missing Message Content Type.");

            RuleFor(x => x.DateSent)
                .NotEmpty()
                .Must(x => x.Kind == DateTimeKind.Utc)
                .WithMessage("Missing date or invalid date format.");

            RuleFor(x => x.ToUserId)
                .NotEmpty()
                .WithMessage("Missing target user id");

        }
    }
}