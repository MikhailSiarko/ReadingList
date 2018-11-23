using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Models.DAO;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class BookStatusesQueryHandler : IRequestHandler<BookStatusesQuery, IEnumerable<SelectListItem>>
    {
        public async Task<IEnumerable<SelectListItem>> Handle(BookStatusesQuery request,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() => BookItemStatus.Read.ToSelectListItems(), cancellationToken);
        }
    }
}