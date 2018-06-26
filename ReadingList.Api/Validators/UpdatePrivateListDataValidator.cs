using FluentValidation;
using ReadingList.Api.Extensions;
using ReadingList.Api.QueriesData;

namespace ReadingList.Api.Validators
{
    public class UpdatePrivateListDataValidator : AbstractValidator<UpdatePrivateListData>
    {
        public UpdatePrivateListDataValidator()
        {
            RuleFor(m => m.Name).NotEmptyWithMessage(m => nameof(m.Name));
        }
    }
}
