using FluentValidation;
using ReadingList.Domain.Commands;
using ReadingList.Models.Read;

namespace ReadingList.Domain.Validators
{
    public class UpdateSharedListItemCommandValidator : AbstractValidator<UpdateSharedListItem>
    {
        public UpdateSharedListItemCommandValidator()
        {
            Include(new SecuredCommandValidator<SharedBookListItemDto>());
        }
    }
}