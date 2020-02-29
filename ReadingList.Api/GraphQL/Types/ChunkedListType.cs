using GraphQL.Types;
using ReadingList.Models.Read;

namespace ReadingList.Api.GraphQL.Types
{
    public class ChunkedListType<TType, TDto> : ObjectGraphType<ChunkedCollectionDto<TDto>> where TType : IGraphType
    {
        public ChunkedListType()
        {
            Field<ChunkInfoType>("chunkInfo", resolve: ctx => ctx.Source.ChunkInfo);
            Field<ListGraphType<TType>>("items", resolve: ctx => ctx.Source.Items);
        }
    }
}