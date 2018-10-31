using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Queries
{
    public class GetSharedListQuery : Query<SharedBookListDto>
    {
        public readonly int ListId;
        
        public GetSharedListQuery(int listId)
        {
            ListId = listId;
        }
    }
}