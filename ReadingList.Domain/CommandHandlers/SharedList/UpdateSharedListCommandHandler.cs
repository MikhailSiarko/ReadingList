using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class UpdateSharedListCommandHandler : UpdateCommandHandler<UpdateSharedListCommand, BookList, SharedBookListPreviewDto>
    {
        public UpdateSharedListCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override SharedBookListPreviewDto Convert(BookList entity)
        {
            return Mapper.Map<BookList, SharedBookListPreviewDto>(entity);
        }

        protected override void Update(BookList entity, UpdateSharedListCommand command)
        {
            entity.Update(new Dictionary<string, object>
            {
                [nameof(BookList.Name)] = command.Name,
//                [nameof(BookList.SharedBookListTags)] =
//                    DbContext.UpdateOrAddSharedListTags(command.Tags, entity).RunSync().ToList()
            });
        }

        protected override async Task<BookList> GetEntity(UpdateSharedListCommand command)
        {
            var list = await WriteService.GetAsync<BookList>(command.ListId);

            if (list == null)
            {
                throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                {
                    ["Id"] = command.UserId.ToString()
                });
            }

            var accessSpecification = new BookListAccessSpecification(list);
            
            if (!accessSpecification.SatisfiedBy(command.UserId))
            {
                throw new AccessDeniedException();
            }        

            return list;
        }
    }
}