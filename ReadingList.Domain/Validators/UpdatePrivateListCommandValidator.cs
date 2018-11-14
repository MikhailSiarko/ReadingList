using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Validators
{
    public class UpdatePrivateListCommandValidator : AbstractValidator<UpdatePrivateListCommand>
    {
        public UpdatePrivateListCommandValidator()
        {
            Include(new SecuredCommandValidator<PrivateBookListDto>());
            RuleFor(m => m.Name).NotEmptyWithMessage(m => nameof(m.Name));
        }
    }
}
