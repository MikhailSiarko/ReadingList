using ReadingList.Models.Read.Abstractions;

namespace ReadingList.Read.Queries
{
    public class GetModeratedLists : CollectionQuery<BookListDto>
    {
        public readonly int UserId;

        public GetModeratedLists(int userId)
        {
            UserId = userId;
        }
    }
}