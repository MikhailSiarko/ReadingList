namespace ReadingList.Application.Commands
{
    public abstract class UpdateCommand<TDto> : SecuredCommand<TDto>
    {
        protected UpdateCommand(string userLogin) : base(userLogin)
        {
        }
    }
}
