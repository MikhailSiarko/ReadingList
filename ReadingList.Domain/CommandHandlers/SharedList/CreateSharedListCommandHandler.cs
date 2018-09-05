using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class CreateSharedListCommandHandler : CommandHandler<CreateSharedListCommand>
    {
        private readonly WriteDbContext _context;

        public CreateSharedListCommandHandler(WriteDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(CreateSharedListCommand command)
        {
            var user = await _context.Users.Include(x => x.BookLists)
                           .SingleOrDefaultAsync(x => x.Login == command.UserLogin) ??
                       throw new ObjectNotExistException<UserWm>(new OnExceptionObjectDescriptor
                       {
                           ["Username"] = command.UserLogin
                       });

            if (user.BookLists.Where(b => b.Type == BookListType.Shared).Any(b => b.Name == command.Name))
                throw new ObjectAlreadyExistsException<BookListWm>(new OnExceptionObjectDescriptor
                {
                    ["Name"] = command.Name
                });
            
            var list = new BookListWm
            {
                Name = command.Name,
                Owner = user,
                OwnerId = user.Id,
                Type = BookListType.Shared
            };

            await _context.BookLists.AddAsync(list);

            await _context.SaveChangesAsync();
        }
    }
}