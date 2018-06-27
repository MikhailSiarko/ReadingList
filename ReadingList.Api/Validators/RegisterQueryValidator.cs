using FluentValidation;
using ReadingList.Api.Extensions;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Api.Validators
{
    public class RegisterQueryValidator : AbstractValidator<RegisterData>
    {
        public RegisterQueryValidator()
        {
            Include(new LoginQueryValidator());
            RuleFor(query => query.ConfirmPassword)
                .NotEmptyWithMessage(m => ValidationMessages.CannotBeEmpty.F(nameof(m.ConfirmPassword)))
                .Matches(query => query.Password).WithMessage(m => ValidationMessages.PasswordsNotConfirm);
        }
    }
}