using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Queries
{
    public class GetPrivateListItemQuery : SecuredQuery<PrivateBookListItemDto>
    {
        public readonly BookInfo BookInfo;

        public GetPrivateListItemQuery(string login, BookInfo bookInfo) : base(login)
        {
            BookInfo = bookInfo;
        }
    }
}