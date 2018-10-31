using FluentValidation;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Queries;
using ReadingList.Resources;

namespace ReadingList.Application.Validators
{
    public class LoginQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(query => query.Login)
                .NotEmptyWithMessage(m => nameof(m.Login))
                .EmailAddress().WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Login)));
            RuleFor(query => query.Password)
                .NotEmptyWithMessage(m => nameof(m.Password));
        }
    }
}