using FluentValidation;
using ReadingList.Api.Extensions;
using ReadingList.Api.QueriesData;

namespace ReadingList.Api.Validators
{
    public class AddItemToPrivateListDataValidator : AbstractValidator<AddItemToPrivateListData>
    {
        public AddItemToPrivateListDataValidator()
        {
            RuleFor(m => m.Author).NotEmptyWithMessage(m => nameof(m.Author));
            RuleFor(m => m.Title).NotEmptyWithMessage(m => nameof(m.Title));
        }
    }
}