using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Models;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class UpdateCommandHandler<TCommand, TEntity, TDto> : CommandHandler<TCommand, TDto>
        where TCommand : UpdateCommand<TDto>
        where TEntity : Entity
    {
        protected UpdateCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected sealed override async Task<TDto> Handle(TCommand command)
        {
            var entity = await GetEntity(command);

            Update(entity, command);

            await WriteService.SaveAsync(entity);

            return Convert(entity);
        }

        protected abstract TDto Convert(TEntity entity);

        protected abstract void Update(TEntity entity, TCommand command);

        protected abstract Task<TEntity> GetEntity(TCommand command);
    }
}