using FluentValidation;
using ReadingList.Domain.Entities;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Infrastructure.Extensions;

namespace ReadingList.Application.Validators
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
