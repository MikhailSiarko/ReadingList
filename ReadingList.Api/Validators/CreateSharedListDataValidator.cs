using FluentValidation;
using ReadingList.Api.Infrastructure.Extensions;
using ReadingList.Api.QueriesData;

namespace ReadingList.Api.Validators
{
    public class CreateSharedListDataValidator : AbstractValidator<CreateSharedListData>
    {
        public CreateSharedListDataValidator()
        {
            RuleFor(x => x.Name).NotEmptyWithMessage(x => nameof(x.Name));
        }
    }
}