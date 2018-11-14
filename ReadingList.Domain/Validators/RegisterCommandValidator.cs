using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Validators
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