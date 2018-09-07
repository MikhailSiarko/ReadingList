using FluentValidation;
using ReadingList.Api.Infrastructure.Extensions;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Api.Validators
{
    public class LoginDataValidator : AbstractValidator<LoginData>
    {
        public LoginDataValidator()
        {
            RuleFor(query => query.Email)
                .NotEmptyWithMessage(m => nameof(m.Email))
                .EmailAddress().WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Email)));
            RuleFor(query => query.Password)
                .NotEmptyWithMessage(m => nameof(m.Password));
        }
    }
}