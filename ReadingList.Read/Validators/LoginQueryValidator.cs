using FluentValidation;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Read.Queries;
using ReadingList.Resources;

namespace ReadingList.Read.Validators
{
    public class LoginQueryValidator : AbstractValidator<LoginUser>
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