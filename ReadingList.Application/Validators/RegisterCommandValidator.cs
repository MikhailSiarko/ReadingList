using FluentValidation;
using ReadingList.Application.Commands;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Application.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(command => command.Email)
                .NotEmptyWithMessage(m => nameof(m.Email))
                .EmailAddress().WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Email)));
            RuleFor(command => command.Password)
                .NotEmptyWithMessage(m => nameof(m.Password));
            RuleFor(command => command.ConfirmPassword)
                .NotEmptyWithMessage(m => ValidationMessages.CannotBeEmpty.F(nameof(m.ConfirmPassword)))
                .Matches(command => command.Password).WithMessage(m => ValidationMessages.PasswordsNotConfirm);
        }
    }
}