using System.Data;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Application.Commands;
using ReadingList.Application.Infrastructure.Behaviors;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Application.MapperProfiles;
using ReadingList.Read;
using ReadingList.Write;

namespace ReadingList.Application
{
    public static class ApplicationModuleExtension
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryBehavior<,>));
            services.AddSingleton<IReadQueriesRegistry, ReadQueriesRegistry>();
            services.AddMediatR(typeof(SecuredCommand).Assembly);
            services.AddDbContextPool<ApplicationDbContext>((provider, builder) =>
                builder.UseSqlite(provider.GetConnectionString("Write")));
            services.AddScoped<IDbConnection, SqliteConnection>(provider =>
                new SqliteConnection(provider.GetConnectionString("Read")));
            services.AddScoped<IApplicationDbConnection, ApplicationDbConnection>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerConfigurator.Configure);
            Mapper.Initialize(conf =>
            {
                conf.AddProfile<PrivateBookListProfile>();
                conf.AddProfile<SharedBookListProfile>();
                conf.AddProfile<UserProfile>();
            });
            return services;
        }
    }
}