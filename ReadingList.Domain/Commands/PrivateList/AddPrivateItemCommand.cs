using ReadingList.Domain.Absrtactions;

namespace ReadingList.Domain.Commands.PrivateList
{
    public class AddPrivateItemCommand : ICommand
    {
        public readonly string Title;
        public readonly string Author;

        public AddPrivateItemCommand(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }
}