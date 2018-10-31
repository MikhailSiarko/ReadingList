using FluentValidation;
using ReadingList.Application.Commands;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Application.Validators
{
    public class SecuredCommandValidator : AbstractValidator<SecuredCommand>
    {
        public SecuredCommandValidator()
        {
            RuleFor(c => c.UserLogin).NotEmptyWithMessage(c => ExceptionMessages.UsernameValidationMessage);
        }
    }
    
    public class SecuredCommandValidator<T> : AbstractValidator<SecuredCommand<T>>
    {
        public SecuredCommandValidator()
        {
            RuleFor(c => c.UserLogin).NotEmptyWithMessage(c => ExceptionMessages.UsernameValidationMessage);
        }
    }
}