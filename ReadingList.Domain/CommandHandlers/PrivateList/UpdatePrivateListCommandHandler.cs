using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class
        UpdatePrivateListCommandHandler : UpdateCommandHandler<UpdatePrivateList, BookList, PrivateBookListDto>
    {
        private readonly IFetchHandler<GetPrivateListByUserId, BookList> _listFetchHandler;

        private readonly IFetchHandler<GetItemsByListId, IEnumerable<PrivateBookListItem>> _itemsFetchHandler;

        public UpdatePrivateListCommandHandler(IDataStorage writeService,
            IFetchHandler<GetPrivateListByUserId, BookList> listFetchHandler,
            IFetchHandler<GetItemsByListId, IEnumerable<PrivateBookListItem>> itemsFetchHandler)
            : base(writeService)
        {
            _listFetchHandler = listFetchHandler;
            _itemsFetchHandler = itemsFetchHandler;
        }

        protected override PrivateBookListDto Convert(BookList entity, UpdatePrivateList command)
        {
            var items = Mapper.Map<IEnumerable<PrivateBookListItem>, IEnumerable<PrivateBookListItemDto>>(
                _itemsFetchHandler.Handle(new GetItemsByListId(entity.Id)).RunSync()).ToList();

            return Mapper.Map<BookList, PrivateBookListDto>(entity,
                options => options.AfterMap((wm, listDto) => listDto.Items = items));
        }

        protected override void Update(BookList entity, UpdatePrivateList command)
        {
            entity.Update(new Dictionary<string, object>
            {
                [nameof(BookList.Name)] = command.Name
            });
        }

        protected override async Task<BookList> GetEntity(UpdatePrivateList command)
        {
            return await _listFetchHandler.Handle(new GetPrivateListByUserId(command.UserId));
        }

        protected override Task Validate(BookList entity, UpdatePrivateList command)
        {
            if (entity == null)
            {
                throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.UserId.ToString()
                });
            }

            return Task.CompletedTask;
        }
    }
}