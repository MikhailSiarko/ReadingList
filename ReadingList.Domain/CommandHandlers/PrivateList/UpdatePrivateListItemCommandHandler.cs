using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Domain.Services.Validation;
using ReadingList.Models.Read;
using ReadingList.Models.Write;

namespace ReadingList.Domain.CommandHandlers
{
    public class UpdatePrivateListItemCommandHandler
        : UpdateCommandHandler<UpdatePrivateListItem, PrivateBookListItem, PrivateBookListItemDto>
    {
        public UpdatePrivateListItemCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override PrivateBookListItemDto Convert(PrivateBookListItem entity, UpdatePrivateListItem command)
        {
            return Mapper.Map<PrivateBookListItem, PrivateBookListItemDto>(entity);
        }

        protected override void Update(PrivateBookListItem entity, UpdatePrivateListItem command)
        {
            entity.Update(new Dictionary<string, object>
            {
                [nameof(PrivateBookListItem.Status)] = command.Status,
                [nameof(PrivateBookListItem.LastStatusUpdateDate)] = DateTimeOffset.UtcNow
            }, (BookItemStatus) command.Status);
        }

        protected override async Task<PrivateBookListItem> GetEntity(UpdatePrivateListItem command)
        {
            return await WriteService.GetAsync<PrivateBookListItem>(command.ItemId);
        }

        protected override Task Validate(PrivateBookListItem entity, UpdatePrivateListItem command)
        {
            if (entity == null)
            {
                throw new ObjectNotExistException<PrivateBookListItem>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.ItemId.ToString()
                });
            }

            var accessSpecification = new BookListOwnerAccessSpecification(entity.BookList);

            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException();
            }

            PrivateBookListItemStatusValidator.Validate(entity.Status, (BookItemStatus) command.Status);

            return Task.CompletedTask;
        }
    }
}