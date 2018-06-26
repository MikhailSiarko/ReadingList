namespace ReadingList.Domain.Commands.PrivateList
{
    public class RemovePrivateItemCommand : SecuredCommand
    {
        public readonly int Id;

        public RemovePrivateItemCommand(int id, string login) : base(login)
        {
            Id = id;
        }
    }
}