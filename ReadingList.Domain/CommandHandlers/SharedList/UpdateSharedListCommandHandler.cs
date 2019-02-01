using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.HelpEntities;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class
        UpdateSharedListCommandHandler : UpdateCommandHandler<UpdateSharedList, BookList,
            SharedBookListDto>
    {
        private readonly IFetchHandler<GetListAccessForUser, (bool editable, bool moderated)> _listAccessFetchHandler;

        private readonly IFetchHandler<GetItemsByListId, IEnumerable<SharedBookListItem>> _itemsFetchHandler;

        private readonly IBookListService _bookListService;

        public UpdateSharedListCommandHandler(IDataStorage writeService,
            IFetchHandler<GetListAccessForUser, (bool editable, bool moderated)> listAccessFetchHandler,
            IFetchHandler<GetItemsByListId, IEnumerable<SharedBookListItem>> itemsFetchHandler, IBookListService bookListService)
            : base(writeService)
        {
            _listAccessFetchHandler = listAccessFetchHandler;
            _itemsFetchHandler = itemsFetchHandler;
            _bookListService = bookListService;
        }

        protected override SharedBookListDto Convert(BookList entity, UpdateSharedList command)
        {
            var items = Mapper.Map<IEnumerable<SharedBookListItem>, IEnumerable<SharedBookListItemDto>>(
                _itemsFetchHandler.Handle(new GetItemsByListId(entity.Id)).RunSync()).ToList();

            var dto = Mapper.Map<BookList, SharedBookListDto>(entity,
                options => options.AfterMap((wm, listDto) => listDto.Items = items));

            var (editable, canBeModerated) = _listAccessFetchHandler
                .Handle(new GetListAccessForUser(command.UserId, dto.Id)).RunSync();

            dto.Editable = editable;

            dto.CanBeModerated = canBeModerated;

            return dto;
        }

        protected override void Update(BookList entity, UpdateSharedList command)
        {
            var tags = _bookListService.ProcessTags(command.Tags, entity.Id).RunSync();

            var moderators = command.Moderators
                ?.Select(m => new BookListModerator
                {
                    UserId = m,
                    BookListId = entity.Id
                })
                .ToList();

            entity.Update(new Dictionary<string, object>
            {
                [nameof(BookList.Name)] = command.Name,
                [nameof(BookList.SharedBookListTags)] = tags ?? entity.SharedBookListTags,
                [nameof(BookList.BookListModerators)] = moderators ?? entity.BookListModerators
            });
        }

        protected override async Task<BookList> GetEntity(UpdateSharedList command)
        {
            return await WriteService.GetAsync<BookList>(command.ListId);
        }

        protected override async Task Validate(BookList entity, UpdateSharedList command)
        {
            if (entity == null)
            {
                throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.UserId.ToString()
                });
            }

            var accessSpecification = new BookListAccessSpecification(entity);

            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ListId.ToString()
                });
            }

            var user = await WriteService.GetAsync<User>(command.UserId);

            if (command.Name != entity.Name)
            {
                var listNameSpecification = new SharedListNameSpecification(user);

                if (!listNameSpecification.SatisfiedBy(command.Name))
                {
                    throw new ObjectAlreadyExistsException<BookList>(new OnExceptionObjectDescriptor
                    {
                        ["Name"] = command.Name
                    });
                }
            }
        }
    }
}