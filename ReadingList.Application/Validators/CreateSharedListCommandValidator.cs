using FluentValidation;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure.Extensions;

namespace ReadingList.Application.Validators
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