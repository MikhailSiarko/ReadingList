using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Models.Read;

namespace ReadingList.Domain.Validators
{
    public class UpdatePrivateListCommandValidator : AbstractValidator<UpdatePrivateList>
    {
        public UpdatePrivateListCommandValidator()
        {
            Include(new SecuredCommandValidator<PrivateBookListDto>());
            RuleFor(m => m.Name).NotEmptyWithMessage(m => nameof(m.Name));
        }
    }
}