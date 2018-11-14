using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class UpdatePrivateListCommandHandler : UpdateCommandHandler<UpdatePrivateListCommand, BookList, PrivateBookListDto>
    {
        private readonly IFetchHandler<GetPrivateListByUserIdQuery, BookList> _listFetchHandler;

        private readonly IFetchHandler<GetItemsByListIdQuery, IEnumerable<PrivateBookListItem>> _itemsFetchHandler;
        
        public UpdatePrivateListCommandHandler(IDataStorage writeService,
            IFetchHandler<GetPrivateListByUserIdQuery, BookList> listFetchHandler, 
            IFetchHandler<GetItemsByListIdQuery, IEnumerable<PrivateBookListItem>> itemsFetchHandler) 
            : base(writeService)
        {
            _listFetchHandler = listFetchHandler;
            _itemsFetchHandler = itemsFetchHandler;
        }

        protected override PrivateBookListDto Convert(BookList entity)
        {
            var items = Mapper.Map<IEnumerable<PrivateBookListItem>, IEnumerable<PrivateBookListItemDto>>(
                _itemsFetchHandler.Fetch(new GetItemsByListIdQuery(entity.Id)).RunSync()).ToList();

            return Mapper.Map<BookList, PrivateBookListDto>(entity,
                options => options.AfterMap((wm, listDto) => listDto.Items = items));
        }

        protected override void Update(BookList entity, UpdatePrivateListCommand command)
        {
            entity.Update(new Dictionary<string, object>
            {
                [nameof(BookList.Name)] = command.Name
            });
        }

        protected override async Task<BookList> GetEntity(UpdatePrivateListCommand command)
        {
            var list = await _listFetchHandler.Fetch(new GetPrivateListByUserIdQuery(command.UserId));
            
            if(list == null)
            {
                throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.UserId.ToString()
                });
            }

            return list;
        }
    }
}