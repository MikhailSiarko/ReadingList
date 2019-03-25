using MediatR;
using ReadingList.Models.Read;

namespace ReadingList.Read.Queries.Book
{
    public class FindBooks : IRequest<ChunkedCollectionDto<BookDto>>
    {
        public readonly string Query;

        public readonly int Chunk;

        public readonly int Count;

        public FindBooks(string query, int? chunk, int? count)
        {
            Query = query;
            Chunk = chunk ?? 1;
            Count = count ?? 15;
        }
    }
}