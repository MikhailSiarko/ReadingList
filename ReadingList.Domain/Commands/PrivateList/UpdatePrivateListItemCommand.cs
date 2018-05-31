namespace ReadingList.Domain.Commands.PrivateList
{
    public class UpdatePrivateListItemCommand : ICommand
    {
        public readonly int ItemId;
        public readonly string Title;
        public readonly string Author;
        public readonly int Status;
        
        public UpdatePrivateListItemCommand(int itemId, string title, string author, int status)
        {
            ItemId = itemId;
            Title = title;
            Author = author;
            Status = status;
        }
    }
}