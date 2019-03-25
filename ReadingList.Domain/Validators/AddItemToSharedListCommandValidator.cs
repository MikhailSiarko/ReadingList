using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Models.Read;

namespace ReadingList.Domain.Validators
{
    public class AddItemToSharedListCommandValidator : AbstractValidator<AddSharedListItem>
    {
        public AddItemToSharedListCommandValidator()
        {
            Include(new SecuredCommandValidator<SharedBookListItemDto>());
            RuleFor(m => m.BookId).NotEqualToDefault();
        }
    }
}