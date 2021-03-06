using FluentValidation;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Read.Queries;

namespace ReadingList.Read.Validators
{
    public class SecuredQueryValidator<T> : AbstractValidator<SecuredQuery<T>>
    {
        public SecuredQueryValidator()
        {
            RuleFor(q => q.UserId).NotEqualToDefault();
        }
    }
}