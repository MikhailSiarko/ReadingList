using System.Collections.Generic;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Write.FetchHandlers;

namespace ReadingList.Write
{
    public static class WriteConfiguration
    {
        public static IServiceCollection RegisterWriteDependencies(this IServiceCollection services)
        {
            services.AddTransient<IFetchHandler<GetBookByAuthorAndTitleQuery, Book>, GetBookFetchHandler>();

            services
                .AddTransient<IFetchHandler<GetBookListItemQuery, PrivateBookListItem>,
                    GetBookListItemFetchHandler<PrivateBookListItem>>();

            services.AddTransient<IFetchHandler<GetUserByLoginQuery, User>, GetUserByLoginFetchHandler>();

            services
                .AddTransient<IFetchHandler<GetBookListItemQuery, SharedBookListItem>,
                    GetBookListItemFetchHandler<SharedBookListItem>>();

            services
                .AddTransient<IFetchHandler<GetItemsByListIdQuery, IEnumerable<PrivateBookListItem>>,
                    GetItemsByListIdFetchHandler<PrivateBookListItem>>();

            services
                .AddTransient<IFetchHandler<GetItemsByListIdQuery, IEnumerable<SharedBookListItem>>,
                    GetItemsByListIdFetchHandler<SharedBookListItem>>();

            services
                .AddTransient<IFetchHandler<GetPrivateListByUserIdQuery, BookList>,
                    GetPrivateListByUserIdFetchHandler>();

            services
                .AddTransient<IFetchHandler<GetSharedListsByUserIdQuery, IEnumerable<BookList>>,
                    GetSharedListsByUserIdFetchHandler>();

            services.AddTransient<IFetchHandler<GetExistingTagsQuery, IEnumerable<Tag>>, GetExistingTagsFetchHandler>();

            services.AddDbContextPool<WriteDbContext>((provider, builder) =>
                builder.UseSqlite(provider.GetConnectionString("Write")));

            services.AddScoped<IDataStorage, DataStorage>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(builder =>
                {
                    builder
                        .AddSQLite()
                        .WithGlobalConnectionString("Write")
                        .ScanIn(typeof(IDomainService).Assembly).For.Migrations();
                });

            MigrateDatabase(services);

            return services;
        }

        private static void MigrateDatabase(IServiceCollection services)
        {
            using (var scope = services.BuildServiceProvider(false).CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                if (runner.HasMigrationsToApplyUp())
                {
                    runner.MigrateUp();
                }
            }
        }
    }
}