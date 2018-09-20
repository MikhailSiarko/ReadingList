using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services.Validation;
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
            var item = await _context.SharedBookListItems
                           .Include(i => i.BookList.Owner)
                           .Include(b => b.BookList.BookListModerators)
                           .ThenInclude(b => b.User).SingleOrDefaultAsync(x =>
                           x.Id == command.ItemId && x.BookListId == command.ListId) ??
                       throw new ObjectNotExistForException<SharedBookListItemWm, BookListWm>(new OnExceptionObjectDescriptor
                           {
                               ["Id"] = command.ItemId.ToString()
                           },
                           new OnExceptionObjectDescriptor
                           {
                               ["Id"] = command.ListId.ToString()
                           });

            BookListAccessValidator.Validate(command.UserLogin, item.BookList);

            _context.SharedBookListItems.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}