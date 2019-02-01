using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginUser>
    {
        public LoginCommandValidator()
        {
            RuleFor(command => command.Email)
                .NotEmptyOrNullWithMessage(m => nameof(m.Email))
                .EmailAddress().WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Email)));
            RuleFor(command => command.Password)
                .NotEmptyOrNullWithMessage(m => nameof(m.Password));
        }
    }
}