using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.HelpEntities;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class CreateSharedListCommandHandler : CommandHandler<CreateSharedListCommand, SharedBookListPreviewDto>
    {
        private readonly IFetchHandler<GetSharedListsByUserIdQuery, IEnumerable<BookList>> _listsFetchHandler;

        private readonly IFetchHandler<GetExistingTagsQuery, IEnumerable<Tag>> _existingTagsFetchHandler;
        
        public CreateSharedListCommandHandler(IDataStorage writeService,
            IFetchHandler<GetSharedListsByUserIdQuery, IEnumerable<BookList>> listsFetchHandler,
            IFetchHandler<GetExistingTagsQuery, IEnumerable<Tag>> existingTagsFetchHandler)
            : base(writeService)
        {
            _listsFetchHandler = listsFetchHandler;
            _existingTagsFetchHandler = existingTagsFetchHandler;
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

            var existingTags = await _existingTagsFetchHandler.Fetch(new GetExistingTagsQuery(command.Tags));

            var newTags = command.Tags
                .Where(t => existingTags.All(e => e.Name != t))
                .Select(t => new Tag
                {
                    Name = t
                })
                .ToList();

            if (newTags.Any())
            {
                await WriteService.SaveRangeAsync(newTags);   
            }

            var list = new BookList
            {
                Name = command.Name,
                OwnerId = user.Id,
                Type = BookListType.Shared
            };

            list.SharedBookListTags = existingTags.Concat(newTags).Select(t => new SharedBookListTag
            {
                Tag = t,
                SharedBookList = list
            }).ToList();

            await WriteService.SaveAsync(list);

            return Mapper.Map<BookList, SharedBookListPreviewDto>(list);
        }
    }
}