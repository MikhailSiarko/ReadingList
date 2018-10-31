using FluentValidation;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Queries;
using ReadingList.Resources;

namespace ReadingList.Application.Validators
{
    public class SecuredQueryValidator<T> : AbstractValidator<SecuredQuery<T>>
    {
        public SecuredQueryValidator()
        {
            RuleFor(q => q.Login).NotEmptyWithMessage(q => ExceptionMessages.UsernameValidationMessage);
        }   
    }
}