using GraphQL.Types;
using ReadingList.Models.Read;

namespace ReadingList.Api.GraphQL.Types
{
    public class ChunkInfoType : ObjectGraphType<ChunkInfo>
    {
        public ChunkInfoType()
        {
            Field(x => x.Chunk);
            Field(x => x.HasNext);
            Field(x => x.HasPrevious);
        }
    }
}