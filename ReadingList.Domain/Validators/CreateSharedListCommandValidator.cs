using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Models.Read;

namespace ReadingList.Domain.Validators
{
    public class CreateSharedListCommandValidator : AbstractValidator<CreateSharedList>
    {
        public CreateSharedListCommandValidator()
        {
            Include(new SecuredCommandValidator<SharedBookListPreviewDto>());
            RuleFor(x => x.Name).NotEmptyWithMessage(x => nameof(x.Name));
        }
    }
}