using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
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
                Type = BookListType.Shared,
                JsonFields = JsonConvert.SerializeObject(new {command.Tags, command.Category})
            };

            await _context.BookLists.AddAsync(list);

            await _context.UpdateOrAddSharedListTags(command.Tags, list);

            if (!await _context.Categories.AnyAsync(c => c.Name == command.Category))
            {
                await _context.AddCategory(command.Category);
            }

            await _context.SaveChangesAsync();
        }
    }
}