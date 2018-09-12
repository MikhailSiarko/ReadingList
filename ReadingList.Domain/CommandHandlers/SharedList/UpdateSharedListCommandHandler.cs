using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReadingList.Domain.Commands.SharedList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers.SharedList
{
    public class UpdateSharedListCommandHandler : CommandHandler<UpdateSharedListCommand>
    {
        private readonly WriteDbContext _dbContext;
        private readonly IEntityUpdateService _updateService;

        public UpdateSharedListCommandHandler(WriteDbContext dbContext, IEntityUpdateService updateService)
        {
            _dbContext = dbContext;
            _updateService = updateService;
        }

        protected override async Task Handle(UpdateSharedListCommand command)
        {
            var list = await _dbContext.BookLists.SingleAsync(l => l.Id == command.ListId && l.Type == BookListType.Shared) ??
                       throw new ObjectNotExistForException<BookListWm, UserWm>(null, new OnExceptionObjectDescriptor
                       {
                           ["Email"] = command.UserLogin
                       });
            
            _updateService.Update(list, new Dictionary<string, object>
            {
                [nameof(BookListWm.Name)] = command.Name,
                [nameof(BookListWm.JsonFields)] = JsonConvert.SerializeObject(new {command.Tags, command.Category})
            });
            
            await _dbContext.SaveChangesAsync();
        }
    }
}