using System;
using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Resources;

namespace ReadingList.Domain.Validators
{
    public class UpdatePrivateListItemCommandValidator : AbstractValidator<UpdatePrivateListItem>
    {
        public UpdatePrivateListItemCommandValidator()
        {
            Include(new SecuredCommandValidator<PrivateBookListItemDto>());
            RuleFor(m => m.Status).Must(s => Enum.IsDefined(typeof(BookItemStatus), s))
                .WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Status)));
        }
    }
}