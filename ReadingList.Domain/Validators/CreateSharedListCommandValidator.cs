using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Validators
{
    public class CreateSharedListCommandValidator : AbstractValidator<CreateSharedListCommand>
    {
        public CreateSharedListCommandValidator()
        {
            Include(new SecuredCommandValidator<SharedBookListPreviewDto>());
            RuleFor(x => x.Name).NotEmptyWithMessage(x => nameof(x.Name));
        }
    }
}