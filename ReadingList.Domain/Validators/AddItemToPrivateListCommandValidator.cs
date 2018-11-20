using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Validators
{
    public class AddItemToPrivateListCommandValidator : AbstractValidator<AddPrivateItemCommand>
    {
        public AddItemToPrivateListCommandValidator()
        {
            Include(new SecuredCommandValidator<PrivateBookListItemDto>());
            RuleFor(m => m.BookId).NotEqualToDefault();
        }
    }
}