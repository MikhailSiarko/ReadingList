namespace ReadingList.Domain.Commands.PrivateList
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