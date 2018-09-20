using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services.Validation;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class DeleteSharedListCommandHandler : CommandHandler<DeleteSharedListCommand>
    {
        private readonly WriteDbContext _context;

        public DeleteSharedListCommandHandler(WriteDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(DeleteSharedListCommand command)
        {
            var list =
                await _context.BookLists.Include(s => s.Owner).SingleOrDefaultAsync(x =>
                    x.Id == command.ListId && x.Type == BookListType.Shared) ??
                throw new ObjectNotExistForException<BookListWm, UserWm>(new OnExceptionObjectDescriptor
                    {
                        ["Id"] = command.ListId.ToString()
                    },
                    new OnExceptionObjectDescriptor
                    {
                        ["Email"] = command.UserLogin
                    });
            
            BookListAccessValidator.Validate(command.UserLogin, list);

            var items = await _context.SharedBookListItems.Where(x => x.BookListId == command.ListId).ToListAsync();
            
            _context.SharedBookListItems.RemoveRange(items);
            
            _context.BookLists.Remove(list);
            
            await _context.SaveChangesAsync();
        }
    }
}