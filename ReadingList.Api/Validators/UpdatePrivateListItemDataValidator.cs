using System;
using FluentValidation;
using ReadingList.Api.QueriesData;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;
using ReadingList.WriteModel.Models;

namespace ReadingList.Api.Validators
{
    public class UpdatePrivateListItemDataValidator : AbstractValidator<UpdatePrivateListItemData>
    {
        public UpdatePrivateListItemDataValidator()
        {
            Include(new AddItemToPrivateListDataValidator());
            RuleFor(m => m.Status).Must(s => Enum.IsDefined(typeof(BookItemStatus), s))
                .WithMessage(m => ValidationMessages.InvalidValue.F(nameof(m.Status)));
        }
    }
}