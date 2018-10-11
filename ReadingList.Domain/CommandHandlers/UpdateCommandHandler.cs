using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Services;
using ReadingList.WriteModel;

namespace ReadingList.Domain.CommandHandlers
{
    public abstract class UpdateCommandHandler<TCommand, TEntity, TDto> : CommandHandler<TCommand, TDto>
        where TCommand : UpdateCommand<TDto>
    {
        protected readonly WriteDbContext DbContext;
        
        protected readonly IEntityUpdateService EntityUpdateService;

        protected UpdateCommandHandler(WriteDbContext dbContext, IEntityUpdateService entityUpdateService)
        {
            DbContext = dbContext;
            EntityUpdateService = entityUpdateService;
        }

        protected sealed override async Task<TDto> Handle(TCommand command)
        {
            var entity = await GetEntity(command);

            Update(entity, command);

            await DbContext.SaveChangesAsync();

            return Convert(entity);
        }

        protected abstract TDto Convert(TEntity entity);

        protected abstract void Update(TEntity entity, TCommand command);

        protected abstract Task<TEntity> GetEntity(TCommand command);
    }
}