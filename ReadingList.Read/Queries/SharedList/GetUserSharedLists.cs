using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class GetUserSharedLists : SecuredQuery<ChunkedCollectionDto<SharedBookListPreviewDto>>
    {
        public readonly int Chunk;

        public readonly int Count;

        public GetUserSharedLists(int userId, int? chunk, int? count) : base(userId)
        {
            Chunk = chunk ?? 1;
            Count = count ?? 15;
        }
    }
}