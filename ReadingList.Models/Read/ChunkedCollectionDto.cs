using System.Collections.Generic;

namespace ReadingList.Models.Read
{
    public class ChunkedCollectionDto<T>
    {
        public ChunkedCollectionDto(IEnumerable<T> items, bool hasNext, int chunkNumber)
        {
            Items = items;
            ChunkInfo = new ChunkInfo(hasNext, chunkNumber);
        }

        public IEnumerable<T> Items { get; }

        public ChunkInfo ChunkInfo { get; set; }

        public static ChunkedCollectionDto<T> Empty => new ChunkedCollectionDto<T>(new List<T>(), false, 1);
    }

    public class ChunkInfo
    {
        public ChunkInfo(bool hasNext, int chunkNumber)
        {
            HasNext = hasNext;
            Chunk = chunkNumber;
        }

        public bool HasNext { get; }

        public bool HasPrevious => Chunk > 1;

        public int Chunk { get; }
    }
}