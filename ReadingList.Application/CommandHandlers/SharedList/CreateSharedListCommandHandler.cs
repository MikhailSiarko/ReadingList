using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Domain.Entities.Identity;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Commands;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.Infrastructure.Filters;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class CreateSharedListCommandHandler : CommandHandler<CreateSharedListCommand, SharedBookListPreviewDto>
    {
        public CreateSharedListCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        protected override async Task<SharedBookListPreviewDto> Handle(CreateSharedListCommand command)
        {
            var user = await DbContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Login == command.UserLogin) ??
                       throw new ObjectNotExistException<User>(new OnExceptionObjectDescriptor
                       {
                           ["Username"] = command.UserLogin
                       });

            if (await DbContext.BookLists.Where(BookListFilterExpressions.SharedBookListsForUser(user.Login))
                .AnyAsync(b => b.Name == command.Name))
            {
                throw new ObjectAlreadyExistsException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Name"] = command.Name
                });
            }

            var list = new BookList
            {
                Name = command.Name,
                OwnerId = user.Id,
                Type = BookListType.Shared
            };

            await DbContext.BookLists.AddAsync(list);

            var listTags = await DbContext.UpdateOrAddSharedListTags(command.Tags, list);

            list.SharedBookListTags = listTags.ToList();

            await DbContext.SaveChangesAsync();

            return Mapper.Map<BookList, SharedBookListPreviewDto>(list);
        }
    }
}