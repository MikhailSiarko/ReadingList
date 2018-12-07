namespace ReadingList.Domain.Commands
{
    public abstract class Update<TDto> : SecuredCommand<TDto>
    {
        protected Update(int userId) : base(userId)
        {
        }
    }
}