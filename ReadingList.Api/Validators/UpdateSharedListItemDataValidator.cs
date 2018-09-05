using FluentValidation;
using ReadingList.Api.QueriesData;

namespace ReadingList.Api.Validators
{
    public class UpdateSharedListItemDataValidator : AbstractValidator<UpdateSharedListItemData>
    {
        public UpdateSharedListItemDataValidator()
        {
            Include(new AddItemToPrivateListDataValidator());
        }
    }
}