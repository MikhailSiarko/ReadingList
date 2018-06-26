using FluentValidation;
using ReadingList.Api.QueriesData;
using ReadingList.Resources;

namespace ReadingList.Api.Validators
{
    public class RegisterQueryValidator : AbstractValidator<RegisterData>
    {
        public RegisterQueryValidator()
        {
            Include(new LoginQueryValidator());
            RuleFor(query => query.ConfirmPassword)
                .NotEmpty().Matches(query => query.Password)
                    .WithMessage(m => ValidationMessages.PasswordsNotConfirm);
        }
    }
}