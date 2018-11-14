using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Validators
{
    public class AddItemToSharedListCommandValidator : AbstractValidator<AddSharedListItemCommand>
    {
        public AddItemToSharedListCommandValidator()
        {
            Include(new SecuredCommandValidator<SharedBookListItemDto>());
            RuleFor(m => m.BookInfo.Author).NotEmptyWithMessage(m => nameof(m.BookInfo.Author));
            RuleFor(m => m.BookInfo.Title).NotEmptyWithMessage(m => nameof(m.BookInfo.Title));
        }
    }
}