namespace ReadingList.Domain.Commands
{
    public abstract class UpdateCommand<TDto> : SecuredCommand<TDto>
    {
        protected UpdateCommand(int userId) : base(userId)
        {
        }
    }
}