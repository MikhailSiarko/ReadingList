using FluentValidation;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure.Extensions;

namespace ReadingList.Application.Validators
{
    public class UpdateSharedListItemCommandValidator : AbstractValidator<UpdateSharedListItemCommand>
    {
        public UpdateSharedListItemCommandValidator()
        {
            Include(new SecuredCommandValidator<SharedBookListItemDto>());
            RuleFor(m => m.BookInfo.Author).NotEmptyWithMessage(m => nameof(m.BookInfo.Author));
            RuleFor(m => m.BookInfo.Title).NotEmptyWithMessage(m => nameof(m.BookInfo.Title));
        }
    }
}