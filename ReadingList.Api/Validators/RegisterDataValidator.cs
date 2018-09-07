using FluentValidation;
using ReadingList.Api.Infrastructure.Extensions;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Api.Validators
{
    public class RegisterDataValidator : AbstractValidator<RegisterData>
    {
        public RegisterDataValidator()
        {
            Include(new LoginDataValidator());
            RuleFor(query => query.ConfirmPassword)
                .NotEmptyWithMessage(m => ValidationMessages.CannotBeEmpty.F(nameof(m.ConfirmPassword)))
                .Matches(query => query.Password).WithMessage(m => ValidationMessages.PasswordsNotConfirm);
        }
    }
}