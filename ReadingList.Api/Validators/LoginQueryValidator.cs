using FluentValidation;
using ReadingList.Api.QueriesData;

namespace ReadingList.Api.Validators
{
    public class LoginQueryValidator : AbstractValidator<LoginData>
    {
        public LoginQueryValidator()
        {
            RuleFor(query => query.Email)
                .NotEmpty().WithMessage("Please specify a email")
                .EmailAddress().WithMessage("Please enter a valid email");
            RuleFor(query => query.Password)
                .NotEmpty().WithMessage("Please specify a password");
        }
    }
}