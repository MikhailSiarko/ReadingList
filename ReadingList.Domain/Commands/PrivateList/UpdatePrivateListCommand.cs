namespace ReadingList.Domain.Commands.PrivateList
{
    public class UpdatePrivateListCommand : SecuredCommand
    {
        public readonly string Name;

        public UpdatePrivateListCommand(string login, string name) : base(login)
        {
            Name = name;
        }
    }
}