namespace ReadingList.Domain.Commands.PrivateList
{
    public class UpdatePrivateListItemCommand : SecuredCommand
    {
        public readonly int ItemId;
        public readonly string Title;
        public readonly string Author;
        public readonly int Status;
        
        public UpdatePrivateListItemCommand(string login, int itemId, string title, string author, int status) : base(login)
        {
            ItemId = itemId;
            Title = title;
            Author = author;
            Status = status;
        }
    }
}