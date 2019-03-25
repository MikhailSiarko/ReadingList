using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterUser>
    {
        public RegisterCommandValidator()
        {
            RuleFor(command => command.Email)
                .NotEmptyOrNullWithMessage(m => nameof(m.Email))
                .EmailAddress().WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Email)));
            RuleFor(command => command.Password)
                .NotEmptyOrNullWithMessage(m => nameof(m.Password));
            RuleFor(command => command.ConfirmPassword)
                .NotEmptyOrNullWithMessage(m => ValidationMessages.CannotBeEmptyOrNull.F(nameof(m.ConfirmPassword)))
                .Matches(command => command.Password).WithMessage(m => ValidationMessages.PasswordsNotConfirm);
        }
    }
}