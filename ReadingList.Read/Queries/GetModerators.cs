using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetModerators : CollectionQuery<ModeratorDto>
    {
        public readonly int UserId;

        public GetModerators(int userId)
        {
            UserId = userId;
        }
    }
}