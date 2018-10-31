using FluentValidation;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Validators;

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