using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class UpdateCommandHandler<TCommand, TEntity, TDto> : CommandHandler<TCommand, TDto>
        where TCommand : Update<TDto>
        where TEntity : Entity
    {
        protected UpdateCommandHandler(IDataStorage writeService) : base(writeService)
        {
        }

        protected sealed override async Task<TDto> Handle(TCommand command)
        {
            var entity = await GetEntity(command);

            await Validate(entity, command);

            Update(entity, command);

            await WriteService.SaveAsync(entity);

            return Convert(entity, command);
        }

        protected abstract TDto Convert(TEntity entity, TCommand command);

        protected abstract void Update(TEntity entity, TCommand command);

        protected abstract Task<TEntity> GetEntity(TCommand command);

        protected abstract Task Validate(TEntity entity, TCommand command);
    }
}