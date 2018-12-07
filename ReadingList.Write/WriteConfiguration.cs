using System.Collections.Generic;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;
using ReadingList.Write.FetchHandlers;

namespace ReadingList.Write
{
    public static class WriteConfiguration
    {
        public static IServiceCollection RegisterWriteDependencies(this IServiceCollection services)
        {
            services
                .AddTransient<IFetchHandler<GetBookListItem, PrivateBookListItem>,
                    GetBookListItemFetchHandler<PrivateBookListItem>>();

            services.AddTransient<IFetchHandler<GetUserByLogin, User>, GetUserByLoginFetchHandler>();

            services
                .AddTransient<IFetchHandler<GetBookListItem, SharedBookListItem>,
                    GetBookListItemFetchHandler<SharedBookListItem>>();

            services
                .AddTransient<IFetchHandler<GetItemsByListId, IEnumerable<PrivateBookListItem>>,
                    GetItemsByListIdFetchHandler<PrivateBookListItem>>();

            services
                .AddTransient<IFetchHandler<GetItemsByListId, IEnumerable<SharedBookListItem>>,
                    GetItemsByListIdFetchHandler<SharedBookListItem>>();

            services
                .AddTransient<IFetchHandler<GetPrivateListByUserId, BookList>,
                    GetPrivateListByUserIdFetchHandler>();

            services
                .AddTransient<IFetchHandler<GetSharedListsByUserId, IEnumerable<BookList>>,
                    GetSharedListsByUserIdFetchHandler>();

            services.AddTransient<IFetchHandler<GetExistingTags, IEnumerable<Tag>>, GetExistingTagsFetchHandler>();

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