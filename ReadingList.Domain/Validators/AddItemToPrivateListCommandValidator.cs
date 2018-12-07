using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Models.Read;

namespace ReadingList.Domain.Validators
{
    public class AddItemToPrivateListCommandValidator : AbstractValidator<AddPrivateItem>
    {
        public AddItemToPrivateListCommandValidator()
        {
            Include(new SecuredCommandValidator<PrivateBookListItemDto>());
            RuleFor(m => m.BookId).NotEqualToDefault();
        }
    }
}