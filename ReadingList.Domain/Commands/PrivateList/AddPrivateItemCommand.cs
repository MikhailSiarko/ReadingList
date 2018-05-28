using ReadingList.Domain.Absrtactions;

namespace ReadingList.Domain.Commands.PrivateList
{
    public class AddPrivateItemCommand : ICommand
    {
        public readonly string Login;  
        public readonly string Title;
        public readonly string Author;

        public AddPrivateItemCommand(string login, string title, string author)
        {
            Login = login;
            Title = title;
            Author = author;
        }
    }
}