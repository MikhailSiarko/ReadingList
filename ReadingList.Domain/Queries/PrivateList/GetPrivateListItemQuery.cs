using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries
{
    public class GetPrivateListItemQuery : SecuredQuery<PrivateBookListItemDto>
    {
        public readonly string Title;
        public readonly string Author;

        public GetPrivateListItemQuery(string login, string title, string author) : base(login)
        {
            Title = title;
            Author = author;
        }
    }
}