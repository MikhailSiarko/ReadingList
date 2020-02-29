using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Models.Write;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetBookStatusesQueryHandler : IRequestHandler<GetBookStatuses, IEnumerable<SelectListItem<int>>>
    {
        public async Task<IEnumerable<SelectListItem<int>>> Handle(GetBookStatuses request,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() => BookItemStatus.Read.ToSelectListItems(), cancellationToken);
        }
    }
}