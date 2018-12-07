using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.HelpEntities;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class CreateSharedListCommandHandler : CommandHandler<CreateSharedList, SharedBookListPreviewDto>
    {
        private readonly IFetchHandler<GetSharedListsByUserId, IEnumerable<BookList>> _listsFetchHandler;

        private readonly IFetchHandler<GetExistingTags, IEnumerable<Tag>> _existingTagsFetchHandler;

        public CreateSharedListCommandHandler(IDataStorage writeService,
            IFetchHandler<GetSharedListsByUserId, IEnumerable<BookList>> listsFetchHandler,
            IFetchHandler<GetExistingTags, IEnumerable<Tag>> existingTagsFetchHandler)
            : base(writeService)
        {
            _listsFetchHandler = listsFetchHandler;
            _existingTagsFetchHandler = existingTagsFetchHandler;
        }

        protected override async Task<SharedBookListPreviewDto> Handle(CreateSharedList command)
        {
            var user = await WriteService.GetAsync<User>(command.UserId);

            if (user == null)
            {
                throw new ObjectNotExistException<User>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.UserId.ToString()
                });
            }

            var lists = await _listsFetchHandler.Handle(new GetSharedListsByUserId(command.UserId));

            if (lists.Any(x => x.Name == command.Name))
            {
                throw new ObjectAlreadyExistsException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Name"] = command.Name
                });
            }

            var existingTags = await _existingTagsFetchHandler.Handle(new GetExistingTags(command.Tags));

            var newTags = command.Tags
                .Where(t => existingTags.All(e => e.Name != t))
                .Select(t => new Tag
                {
                    Name = t
                })
                .ToList();

            if (newTags.Any())
            {
                await WriteService.SaveBatchAsync(newTags);
            }

            var list = new BookList
            {
                Name = command.Name,
                OwnerId = user.Id,
                Type = BookListType.Shared
            };

            list.SharedBookListTags = existingTags
                .Concat(newTags)
                .Select(t => new SharedBookListTag
                {
                    Tag = t,
                    SharedBookList = list
                })
                .ToList();

            await WriteService.SaveAsync(list);

            return Mapper.Map<BookList, SharedBookListPreviewDto>(list);
        }
    }
}