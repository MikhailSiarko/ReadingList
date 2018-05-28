using ReadingList.Domain.Absrtactions;

namespace ReadingList.Domain.Commands.PrivateList
{
    public class UpdatePrivateListCommand : ICommand
    {
        public readonly string Name;
        public readonly string Login;

        public UpdatePrivateListCommand(string login, string name)
        {
            Name = name;
            Login = login;
        }
    }
}