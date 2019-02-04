using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries
{
    public class FindSharedLists : IRequest<ChunkedCollectionDto<SharedBookListPreviewDto>>
    {
        public readonly string Query;

        public readonly int Chunk;

        public readonly int Count;

        public FindSharedLists(string query, int? chunk, int? count)
        {
            Query = query;
            Chunk = chunk ?? 1;
            Count = count ?? 15;
        }
    }
}