using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Queries.SharedList
{
    public class GetSharedListItemQuery : SecuredQuery<SharedBookListItemDto>
    {
        private readonly BookInfo BookInfo;
        
        public GetSharedListItemQuery(string login, BookInfo bookInfo) : base(login)
        {
            BookInfo = bookInfo;
        }
    }
}