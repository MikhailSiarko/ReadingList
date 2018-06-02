using ReadingList.Domain.DTO.BookList;
using PrivateBookListItemRm = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.Queries
{
    public class GetPrivateListItemQuery : IQuery<PrivateBookListItemDto>
    {
        public readonly string Login;
        public readonly string Title;
        public readonly string Author;

        public GetPrivateListItemQuery(string login, string title, string author)
        {
            Login = login;
            Title = title;
            Author = author;
        }
    }
}