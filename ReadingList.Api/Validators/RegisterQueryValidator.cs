using FluentValidation;
using ReadingList.Api.QueriesData;

namespace ReadingList.Api.Validators
{
    public class RegisterQueryValidator : AbstractValidator<RegisterData>
    {
        public RegisterQueryValidator()
        {
            RuleFor(query => query.Email)
                .NotEmpty().WithMessage("Please specify a email")
                .EmailAddress().WithMessage("Please enter a valid email");
            RuleFor(query => query.Password)
                .NotEmpty().WithMessage("Please specify a password");
            RuleFor(query => query.ConfirmPassword)
                .NotEmpty().Matches(query => query.Password)
                    .WithMessage("Passwords do not confirm");
        }
    }
}