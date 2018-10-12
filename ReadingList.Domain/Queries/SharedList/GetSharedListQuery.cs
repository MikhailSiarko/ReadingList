using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries
{
    public class GetSharedListQuery : IQuery<SharedListDto>
    {
        public readonly int ListId;
        
        public GetSharedListQuery(int listId)
        {
            ListId = listId;
        }
    }
}