using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.Validators
{
    public class UpdateSharedListItemCommandValidator : AbstractValidator<UpdateSharedListItemCommand>
    {
        public UpdateSharedListItemCommandValidator()
        {
            Include(new SecuredCommandValidator<SharedBookListItemDto>());
        }
    }
}