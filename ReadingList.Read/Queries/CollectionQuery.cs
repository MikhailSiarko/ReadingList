using System.Collections.Generic;
using MediatR;

namespace ReadingList.Read.Queries
{
    public class CollectionQuery<T> : IRequest<IEnumerable<T>>
    {
    }
}