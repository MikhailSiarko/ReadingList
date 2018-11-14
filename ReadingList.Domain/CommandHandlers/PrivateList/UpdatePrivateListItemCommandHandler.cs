using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Domain.Services.Validation;

namespace ReadingList.Domain.CommandHandlers
{
    public class UpdatePrivateListItemCommandHandler
        : UpdateCommandHandler<UpdatePrivateListItemCommand, PrivateBookListItem, PrivateBookListItemDto>
    {
        public UpdatePrivateListItemCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override PrivateBookListItemDto Convert(PrivateBookListItem entity)
        {
            return Mapper.Map<PrivateBookListItem, PrivateBookListItemDto>(entity);
        }

        protected override void Update(PrivateBookListItem entity, UpdatePrivateListItemCommand command)
        {
            entity.Update(new Dictionary<string, object>
            {
                [nameof(PrivateBookListItem.Status)] = command.Status,
                [nameof(PrivateBookListItem.LastStatusUpdateDate)] = DateTimeOffset.UtcNow
            }, (BookItemStatus) command.Status);
        }

        protected override async Task<PrivateBookListItem> GetEntity(UpdatePrivateListItemCommand command)
        {
            var item = await WriteService.GetAsync<PrivateBookListItem>(command.ItemId);
            
            if(item == null)
            {
                throw new ObjectNotExistException<PrivateBookListItem>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ItemId.ToString()
                });
            }

            var accessSpecification = new BookListAccessSpecification(item.BookList);
            
            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException();
            }

            PrivateBookListItemStatusValidator.Validate(item.Status, (BookItemStatus)command.Status);

            return item;
        }
    }
}