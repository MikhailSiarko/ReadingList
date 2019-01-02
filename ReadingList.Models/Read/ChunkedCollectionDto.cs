using System.Collections.Generic;

namespace ReadingList.Models.Read
{
    public class ChunkedCollectionDto<T>
    {
        public ChunkedCollectionDto(IEnumerable<T> items, bool hasNext, int chunkNumber)
        {
            Items = items;
            HasNext = hasNext;
            Chunk = chunkNumber;
        }

        public IEnumerable<T> Items { get; }

        public bool HasNext { get; }

        public bool HasPrevious => Chunk > 1;

        public int Chunk { get; }

        public static ChunkedCollectionDto<T> Empty => new ChunkedCollectionDto<T>(new List<T>(), false, 1);
    }
}