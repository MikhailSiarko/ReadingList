using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.Domain.Infrastructure.Filters.ValidationFilters;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public class DeletePrivateItemCommandHandler : CommandHandler<DeletePrivateItemCommand>
    {
        private readonly WriteDbContext _dbContext;

        public DeletePrivateItemCommandHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePrivateItemCommand command)
        {
            var itemQuery =
                _dbContext.PrivateBookListItems.Where(EntityFilterExpressions.FindEntity<PrivateBookListItemWm>(command.Id));

            var item = await itemQuery.SingleOrDefaultAsync() ??
                       throw new ObjectNotExistException<PrivateBookListItemWm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = command.Id.ToString()
                       });

            if (!await itemQuery.AnyAsync(BookListAccessValidationFilterExpression.UserIsOwnerOfItem<PrivateBookListItemWm>(command.UserLogin)))
            {
                throw new AccessDeniedException();
            }

            _dbContext.PrivateBookListItems.Remove(item);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}