namespace ReadingList.Domain.Commands
{
    public class DeletePrivateItemCommand : SecuredCommand
    {
        public readonly int Id;

        public DeletePrivateItemCommand(int id, string login) : base(login)
        {
            Id = id;
        }
    }
}