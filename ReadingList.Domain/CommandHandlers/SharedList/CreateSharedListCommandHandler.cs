using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class CreateSharedListCommandHandler : CommandHandler<CreateSharedList, SharedBookListPreviewDto>
    {
        private readonly IBookListService _bookListService;
        public CreateSharedListCommandHandler(IDataStorage writeService, IBookListService bookListService) : base(writeService)
        {
            _bookListService = bookListService;
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

            var list = new BookList
            {
                Name = command.Name,
                OwnerId = user.Id,
                Type = BookListType.Shared
            };

            var tags = await _bookListService.ProcessTags(command.Tags, list.Id);

            if (tags != null)
            {
                list.SharedBookListTags = tags;
            }

            await WriteService.SaveAsync(list);

            return Mapper.Map<BookList, SharedBookListPreviewDto>(list);
        }
    }
}