namespace ReadingList.Domain.Commands
{
    public abstract class UpdateCommand<TDto> : SecuredCommand<TDto>
    {
        protected UpdateCommand(string userLogin) : base(userLogin)
        {
        }
    }
}
