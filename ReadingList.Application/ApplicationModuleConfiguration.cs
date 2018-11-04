using System;
using System.Collections.Generic;
using System.Data;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Application.Authentication.AuthenticationOptions;
using ReadingList.Application.Commands;
using ReadingList.Application.Infrastructure.Behaviors;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.MapperProfiles;
using ReadingList.Application.Queries;
using ReadingList.Application.Queries.SharedList;
using ReadingList.Application.Services;
using ReadingList.Application.Services.Authentication;
using ReadingList.Application.Services.Encryption;
using ReadingList.Read;
using ReadingList.Read.SqlQueries;
using ReadingList.Write;

namespace ReadingList.Application
{
    public static class ApplicationModuleConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region Register MediatR and its Pipeline behaviors

            services.AddMediatR(typeof(SecuredCommand).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryBehavior<,>));

            #endregion

            #region Register Services

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddSingleton<IEntityUpdateService, EntityUpdateService>();
            services.AddSingleton<IReadQueriesRegistry, ReadQueriesRegistry>(_ => new ReadQueriesRegistry(ReadQueriesMap));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerConfigurator.Configure);

            #endregion

            #region Configure and register data access infrastructure

            services.AddDbContextPool<ApplicationDbContext>((provider, builder) =>
                builder.UseSqlite(provider.GetConnectionString("Write")));
            services.AddScoped<IDbConnection, SqliteConnection>(provider =>
                new SqliteConnection(provider.GetConnectionString("Read")));
            services.AddScoped<IApplicationDbConnection, ApplicationDbConnection>();

            #endregion
           
            Mapper.Initialize(conf =>
            {
                conf.AddProfile<PrivateBookListProfile>();
                conf.AddProfile<SharedBookListProfile>();
                conf.AddProfile<UserProfile>();
            });
            return services;
        }

        private static IReadOnlyDictionary<Type, string> ReadQueriesMap => new Dictionary<Type, string>
        {
            // User
            [typeof(LoginUserQuery)] = UserSqlQueries.SelectByLogin,
            [typeof(GetUserQuery)] = UserSqlQueries.SelectById,
            // Shared
            [typeof(FindSharedListsQuery)] = SharedListSqlQueries.SelectPreviews, // TODO change after search logic implementation
            [typeof(GetSharedListQuery)] = SharedListSqlQueries.SelectById,
            [typeof(GetSharedListItemsQuery)] = SharedItemSqlQueries.SelectByListId,
            [typeof(GetSharedListItemQuery)] = SharedItemSqlQueries.SelectById,
            [typeof(GetUserSharedListsQuery)] = SharedListSqlQueries.SelectOwn,
            // Private
            [typeof(GetPrivateListQuery)] = PrivateListSqlQueries.SelectByLogin,
            [typeof(GetPrivateListItemQuery)] = PrivateItemSqlQueries.SelectById
        };
    }
}