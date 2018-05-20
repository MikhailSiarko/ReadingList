using FluentValidation;
using ReadingList.Api.Queries;

namespace ReadingList.Api.Validators
{
    public class RegisterQueryValidator : AbstractValidator<RegisterQuery>
    {
        public RegisterQueryValidator()
        {
            RuleFor(query => query.Email)
                .NotEmpty().WithMessage("Please specify a email")
                .EmailAddress().WithMessage("Please enter a valid email");
            RuleFor(query => query.Password)
                .NotEmpty().WithMessage("Please specify a password");
        }
    }
}