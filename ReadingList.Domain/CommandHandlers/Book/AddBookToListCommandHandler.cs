using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class AddBookToListCommandHandler : CommandHandler<AddBookToList>
    {
        public AddBookToListCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override async Task Handle(AddBookToList command)
        {
            var user = await WriteService.GetAsync<User>(command.UserId);

            var list = await GetList(user, command);

            Validate(list, user);

            var item = CreateItem(list, command);

            await WriteService.SaveAsync(item);
        }

        private static BookListItem CreateItem(BookList list, AddBookToList command)
        {
            BookListItem item;

            if (list.Type == BookListType.Shared)
            {
                item = new SharedBookListItem
                {
                    BookListId = list.Id,
                    BookId = command.BookId
                };
            }
            else
            {
                item = new PrivateBookListItem
                {
                    BookId = command.BookId,
                    BookListId = list.Id
                };
            }

            return item;
        }

        private async Task<BookList> GetList(User user, AddBookToList command)
        {
            BookList list;

            if (user.BookLists.Any(b => b.Id == command.ListId))
            {
                list = user.BookLists.Single(b => b.Id == command.ListId);
            }
            else
            {
                list = await WriteService.GetAsync<BookList>(command.ListId);
            }

            return list;
        }

        private static void Validate(BookList list, User user)
        {
            var accessSpecification = new BookListAccessSpecification(list);

            if (!accessSpecification.SatisfiedBy(user.Id))
            {
                throw new AccessDeniedException();
            }
        }
    }
}