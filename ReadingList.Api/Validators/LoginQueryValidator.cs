using FluentValidation;
using ReadingList.Api.Extensions;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Api.Validators
{
    public class LoginQueryValidator : AbstractValidator<LoginData>
    {
        public LoginQueryValidator()
        {
            RuleFor(query => query.Email)
                .NotEmptyWithMessage(m => nameof(m.Email))
                .EmailAddress().WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Email)));
            RuleFor(query => query.Password)
                .NotEmptyWithMessage(m => nameof(m.Password));
        }
    }
}