using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Application.Commands;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Application.Infrastructure.Filters.ValidationFilters;
using ReadingList.Domain.Entities;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class DeletePrivateItemCommandHandler : CommandHandler<DeletePrivateItemCommand>
    {
        public DeletePrivateItemCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override async Task Handle(DeletePrivateItemCommand command)
        {
            var itemQuery =
                DbContext.PrivateBookListItems.Where(EntityFilterExpressions.WithId<PrivateBookListItem>(command.Id));

            var item = await itemQuery.SingleOrDefaultAsync() ??
                       throw new ObjectNotExistException<PrivateBookListItem>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.Id.ToString()
                       });

            if (!await itemQuery.AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOfItem<PrivateBookListItem>(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            DbContext.PrivateBookListItems.Remove(item);
            
            await DbContext.SaveChangesAsync();
        }
    }
}