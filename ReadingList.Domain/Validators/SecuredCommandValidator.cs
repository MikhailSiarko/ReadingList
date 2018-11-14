using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Validators
{
    public class SecuredCommandValidator : AbstractValidator<SecuredCommand>
    {
        public SecuredCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmptyWithMessage(c => ExceptionMessages.UserIdValidationMessage);
        }
    }
    
    public class SecuredCommandValidator<T> : AbstractValidator<SecuredCommand<T>>
    {
        public SecuredCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmptyWithMessage(c => ExceptionMessages.UserIdValidationMessage);
        }
    }
}