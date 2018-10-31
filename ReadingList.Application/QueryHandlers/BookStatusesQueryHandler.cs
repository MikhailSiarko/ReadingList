using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Infrastructure;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Queries;

namespace ReadingList.Application.QueryHandlers
{
    public class BookStatusesQueryHandler : IRequestHandler<BookStatusesQuery, IEnumerable<SelectListItem>>
    {
        public async Task<IEnumerable<SelectListItem>> Handle(BookStatusesQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => BookItemStatus.Read.ToSelectListItems(), cancellationToken);
        }
    }
}