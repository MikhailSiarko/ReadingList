using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class CreateSharedListCommandHandler : CommandHandler<CreateSharedListCommand, SharedBookListPreviewDto>
    {
        private readonly IFetchHandler<GetSharedListsByUserIdQuery, IEnumerable<BookList>> _listsFetchHandler;
        
        public CreateSharedListCommandHandler(IDataStorage writeService,
            IFetchHandler<GetSharedListsByUserIdQuery, IEnumerable<BookList>> listsFetchHandler) 
            : base(writeService)
        {
            _listsFetchHandler = listsFetchHandler;
        }

        protected override async Task<SharedBookListPreviewDto> Handle(CreateSharedListCommand command)
        {
            var user = await WriteService.GetAsync<User>(command.UserId);
            
            if(user == null)
            {
                throw new ObjectNotExistException<User>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.UserId.ToString()
                });
            }

            var lists = await _listsFetchHandler.Fetch(new GetSharedListsByUserIdQuery(command.UserId));
            
            if (lists.Any(x => x.Name == command.Name))
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

            await WriteService.SaveAsync(list);

            // TODO Implement tags adding
            
            // var listTags = await DbContext.UpdateOrAddSharedListTags(command.Tags, list);

            // list.SharedBookListTags = listTags.ToList();

            return Mapper.Map<BookList, SharedBookListPreviewDto>(list);
        }
    }
}