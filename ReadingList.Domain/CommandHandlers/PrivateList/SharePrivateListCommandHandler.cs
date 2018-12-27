using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class SharePrivateListCommandHandler : CommandHandler<SharePrivateList>
    {
        private readonly IFetchHandler<GetPrivateListByUserId, BookList> _privateListFetchHandler;

        private readonly IFetchHandler<GetItemsByListId, IEnumerable<PrivateBookListItem>> _privateItemsFetchHandler;

        public SharePrivateListCommandHandler(IDataStorage writeService,
            IFetchHandler<GetPrivateListByUserId, BookList> privateListFetchHandler,
            IFetchHandler<GetItemsByListId, IEnumerable<PrivateBookListItem>> privateItemsFetchHandler) : base(
            writeService)
        {
            _privateListFetchHandler = privateListFetchHandler;
            _privateItemsFetchHandler = privateItemsFetchHandler;
        }

        protected override async Task Handle(SharePrivateList command)
        {
            var privateList = await _privateListFetchHandler.Handle(new GetPrivateListByUserId(command.UserId));

            var sharedList = CreateSharedListFromPrivate(privateList, command);

            var user = await WriteService.GetAsync<User>(command.UserId);

            var listNameSpecification = new SharedListNameSpecification(user);

            if (!listNameSpecification.SatisfiedBy(sharedList.Name))
            {
                throw new ObjectAlreadyExistsException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Name"] = command.Name
                });
            }

            await WriteService.SaveAsync(sharedList);

            var privateItems = await _privateItemsFetchHandler.Handle(new GetItemsByListId(privateList.Id));

            var sharedItems = CreateSharedItemsFromPrivateItems(privateItems, sharedList.Id);

            await WriteService.SaveBatchAsync(sharedItems);
        }

        private static BookList CreateSharedListFromPrivate(BookList privateList, SharePrivateList command)
        {
            return new BookList
            {
                Name = string.IsNullOrEmpty(command.Name) ? privateList.Name : command.Name,
                Type = BookListType.Shared,
                OwnerId = privateList.OwnerId
            };
        }

        private static IEnumerable<SharedBookListItem> CreateSharedItemsFromPrivateItems(
            IEnumerable<PrivateBookListItem> privateItems, int sharedListId)
        {
            return privateItems.Select(i => new SharedBookListItem
            {
                BookListId = sharedListId,
                BookId = i.BookId
            });
        }
    }
}