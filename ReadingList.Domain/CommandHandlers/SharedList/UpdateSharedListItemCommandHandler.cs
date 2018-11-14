using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class UpdateSharedListItemCommandHandler 
        : UpdateCommandHandler<UpdateSharedListItemCommand, SharedBookListItem, SharedBookListItemDto>
    {
        public UpdateSharedListItemCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override SharedBookListItemDto Convert(SharedBookListItem entity)
        {
            return Mapper.Map<SharedBookListItem, SharedBookListItemDto>(entity);
        }

        protected override void Update(SharedBookListItem entity, UpdateSharedListItemCommand command)
        {
//            entity.Update(new Dictionary<string, object>
//            {
////                [nameof(SharedBookListItem.SharedBookListItemTags)] =
////                    DbContext.UpdateOrAddSharedListItemTags(command.Tags, entity).RunSync().ToList()
//            });
        }

        protected override async Task<SharedBookListItem> GetEntity(UpdateSharedListItemCommand command)
        {
            var item = await WriteService.GetAsync<SharedBookListItem>(command.ItemId);

            if (item == null)
            {
                throw new ObjectNotExistException<SharedBookListItem>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ItemId.ToString()
                });
            }

            var accessSpecification = new BookListAccessSpecification(item.BookList);

            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException();
            }

            return item;
        }
    }
}