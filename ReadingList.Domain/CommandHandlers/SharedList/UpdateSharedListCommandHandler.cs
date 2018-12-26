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
        private readonly IFetchHandler<GetListAccessForUser, bool> _listAccessFetchHandler;

        private IEnumerable<Tag> _tags;

        public UpdateSharedListCommandHandler(IDataStorage writeService, IFetchHandler<GetListAccessForUser, bool> listAccessFetchHandler)
            : base(writeService)
        {
            _listAccessFetchHandler = listAccessFetchHandler;
            _tags = new List<Tag>();
        }

        protected override SharedBookListDto Convert(BookList entity, UpdateSharedList command)
        {
            foreach (var listTag in entity.SharedBookListTags)
            {
                listTag.Tag = _tags.Single(t => t.Id == listTag.TagId);
            }

            var dto = Mapper.Map<BookList, SharedBookListDto>(entity);

            dto.Editable = _listAccessFetchHandler.Handle(new GetListAccessForUser(command.UserId)).RunSync();

            return dto;
        }

        protected override void Update(BookList entity, UpdateSharedList command)
        {
            var existingTags = command.Tags
                .Where(t => t.Id != default(int))
                .ToList();

            var newTags = command.Tags
                .Where(t => t.Id == default(int))
                .ToList();

            if (newTags.Any())
            {
                WriteService.SaveBatchAsync(newTags).RunSync();
            }

            _tags = existingTags.Concat(newTags);

            var tags = _tags
                .Select(t => new SharedBookListTag
                {
                    TagId = t.Id,
                    SharedBookListId = entity.Id
                })
                .ToList();

            entity.Update(new Dictionary<string, object>
            {
                [nameof(BookList.Name)] = command.Name,
                [nameof(BookList.SharedBookListTags)] = tags
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
                throw new AccessDeniedException();
            }

            var user = await WriteService.GetAsync<User>(command.UserId);

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