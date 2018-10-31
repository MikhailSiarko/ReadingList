using FluentValidation;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure.Extensions;

namespace ReadingList.Application.Validators
{
    public class AddItemToPrivateListCommandValidator : AbstractValidator<AddPrivateItemCommand>
    {
        public AddItemToPrivateListCommandValidator()
        {
            Include(new SecuredCommandValidator<PrivateBookListItemDto>());
            RuleFor(m => m.BookInfo.Author).NotEmptyWithMessage(m => nameof(m.BookInfo.Author));
            RuleFor(m => m.BookInfo.Title).NotEmptyWithMessage(m => nameof(m.BookInfo.Title));
        }
    }
}