using System.Collections.Generic;
using MediatR;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Read.Queries
{
    public class GetBookStatuses : IRequest<IEnumerable<SelectListItem>>
    {
    }
}