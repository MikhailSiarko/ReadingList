namespace ReadingList.Domain.Commands.PrivateList
{
    public class AddPrivateItemCommand : SecuredCommand
    {
        public readonly string Title;
        public readonly string Author;

        public AddPrivateItemCommand(string login, string title, string author) : base(login)
        {
            Title = title;
            Author = author;
        }
    }
}