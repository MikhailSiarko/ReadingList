using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Queries;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.QueryHandlers
{
    public class BookStatusesQueryHandler : QueryHandler<BookStatusesQuery, IEnumerable<SelectListItem>>
    {
        protected override async Task<IEnumerable<SelectListItem>> Handle(BookStatusesQuery query)
        {
            return await Task.Run(() => BookItemStatus.Read.ToSelectListItems());
        }
    }
}