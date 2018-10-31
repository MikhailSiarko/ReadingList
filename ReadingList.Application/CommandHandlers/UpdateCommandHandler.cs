using System.Threading.Tasks;
using ReadingList.Application.Commands;
using ReadingList.Application.Services;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public abstract class UpdateCommandHandler<TCommand, TEntity, TDto> : CommandHandler<TCommand, TDto>
        where TCommand : UpdateCommand<TDto>
    {
        protected readonly IEntityUpdateService EntityUpdateService;

        protected UpdateCommandHandler(ApplicationDbContext dbContext, IEntityUpdateService entityUpdateService) : base(dbContext)
        {
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