using FluentValidation;
using ReadingList.Api.Extensions;
using ReadingList.Api.QueriesData;

namespace ReadingList.Api.Validators
{
    public class AddItemToSharedListDataValidator : AbstractValidator<AddItemToSharedListData>
    {
        public AddItemToSharedListDataValidator()
        {
            Include(new AddItemToPrivateListDataValidator());
            // RuleFor(x => x.ListId).NotEqualToDefault();
        }
    }
}