using System;
using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Resources;

namespace ReadingList.Domain.Validators
{
    public class UpdatePrivateListItemCommandValidator : AbstractValidator<UpdatePrivateListItemCommand>
    {
        public UpdatePrivateListItemCommandValidator()
        {
            Include(new SecuredCommandValidator<PrivateBookListItemDto>());
            RuleFor(m => m.Status).Must(s => Enum.IsDefined(typeof(BookItemStatus), s))
                .WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Status)));
        }
    }
}