using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Specifications;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class
        UpdateSharedListCommandHandler : UpdateCommandHandler<UpdateSharedList, BookList,
            SharedBookListPreviewDto>
    {
        public UpdateSharedListCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected override SharedBookListPreviewDto Convert(BookList entity)
        {
            return Mapper.Map<BookList, SharedBookListPreviewDto>(entity);
        }

        protected override void Update(BookList entity, UpdateSharedList command)
        {
            entity.Update(new Dictionary<string, object>
            {
                [nameof(BookList.Name)] = command.Name
            });
        }

        protected override async Task<BookList> GetEntity(UpdateSharedList command)
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