using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Infrastructure.Filters;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
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
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Login == command.UserLogin) ??
                       throw new ObjectNotExistException<UserWm>(new OnExceptionObjectDescriptor
                       {
                           ["Username"] = command.UserLogin
                       });

            if (await _context.BookLists.Where(BookListFilterExpressions.FindSharedBookListsForUser(user.Id))
                .AnyAsync(b => b.Name == command.Name))
            {
                throw new ObjectAlreadyExistsException<BookListWm>(new OnExceptionObjectDescriptor
                {
                    ["Name"] = command.Name
                });
            }

            var list = new BookListWm
            {
                Name = command.Name,
                OwnerId = user.Id,
                Type = BookListType.Shared
            };

            await _context.BookLists.AddAsync(list);

            var listTags = await _context.UpdateOrAddSharedListTags(command.Tags, list);

            list.SharedBookListTags = listTags.ToList();

            await _context.SaveChangesAsync();
        }
    }
}