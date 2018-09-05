using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class DeleteSharedListItemCommandHandler : CommandHandler<DeleteSharedListItemCommand>
    {
        private readonly WriteDbContext _context;

        public DeleteSharedListItemCommandHandler(WriteDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(DeleteSharedListItemCommand command)
        {
            if (!await _context.BookLists.AnyAsync(x =>
                x.Owner.Login == command.UserLogin && x.Id == command.ListId && x.Type == BookListType.Shared))
            {
                throw new ObjectNotExistForException<BookListWm, UserWm>(new OnExceptionObjectDescriptor
                    {
                        ["Id"] = command.ListId.ToString()
                    },
                    new OnExceptionObjectDescriptor
                    {
                        ["Email"] = command.UserLogin
                    });
            }

            var item = await _context.SharedBookListItems.SingleOrDefaultAsync(x =>
                           x.Id == command.ItemId && x.BookListId == command.ListId) ??
                       throw new ObjectNotExistForException<SharedBookListItemWm, BookListWm>(new OnExceptionObjectDescriptor
                           {
                               ["Id"] = command.ItemId.ToString()
                           },
                           new OnExceptionObjectDescriptor
                           {
                               ["Id"] = command.ListId.ToString()
                           });

            _context.SharedBookListItems.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}