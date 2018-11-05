using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.Queries
{
    public class GetSharedListQuery : SecuredQuery<SharedBookListDto>
    {
        public readonly int ListId;
        
        public GetSharedListQuery(int listId, string login) : base(login)
        {
            ListId = listId;
        }
    }
}