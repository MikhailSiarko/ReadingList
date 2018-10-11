using System.Data;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.MapperProfiles;
using ReadingList.ReadModel;
using ReadingList.WriteModel;

namespace ReadingList.Domain
{
    public static class DomainModuleExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ICommand).Assembly);
            services.AddDbContext<WriteDbContext>((provider, builder) =>
                builder.UseSqlite(provider.GetConnectionString("Write")));
            services.AddScoped<IDbConnection, SqliteConnection>(provider =>
                new SqliteConnection(provider.GetConnectionString("Read")));
            services.AddScoped<IDbReader, DbReader>();
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