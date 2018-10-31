using System;
using FluentValidation;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Application.Validators
{
    public class UpdatePrivateListItemCommandValidator : AbstractValidator<UpdatePrivateListItemCommand>
    {
        public UpdatePrivateListItemCommandValidator()
        {
            Include(new SecuredCommandValidator<PrivateBookListItemDto>());
            RuleFor(m => m.BookInfo.Author).NotEmptyWithMessage(m => nameof(m.BookInfo.Author));
            RuleFor(m => m.BookInfo.Title).NotEmptyWithMessage(m => nameof(m.BookInfo.Title));
            RuleFor(m => m.Status).Must(s => Enum.IsDefined(typeof(BookItemStatus), s))
                .WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Status)));
        }
    }
}