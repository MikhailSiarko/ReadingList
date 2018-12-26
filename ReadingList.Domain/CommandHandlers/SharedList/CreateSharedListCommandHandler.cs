using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.HelpEntities;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class CreateSharedListCommandHandler : CommandHandler<CreateSharedList, SharedBookListPreviewDto>
    {
        public CreateSharedListCommandHandler(IDataStorage writeService) : base(writeService)
        {
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

            var listNameSpecification = new SharedListNameSpecification(user);

            if (!listNameSpecification.SatisfiedBy(command.Name))
            {
                throw new ObjectAlreadyExistsException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Name"] = command.Name
                });
            }

            var existingTags = command.Tags
                .Where(t => t.Id != default(int))
                .ToList();

            var newTags = command.Tags
                .Where(t => t.Id == default(int))
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
                    TagId = t.Id,
                    SharedBookListId = list.Id
                })
                .ToList();

            await WriteService.SaveAsync(list);

            return Mapper.Map<BookList, SharedBookListPreviewDto>(list);
        }
    }
}