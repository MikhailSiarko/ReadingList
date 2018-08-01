using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Authentication.AuthenticationOptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.MapperProfiles;
using ReadingList.Domain.Services;
using ReadingList.Domain.Services.Authentication;
using ReadingList.Domain.Services.Encryption;
using ReadingList.Domain.Services.Sql;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel;

namespace ReadingList.Domain
{
    public static class DomainModuleExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IUserSqlService, UserSqlService>();
            services.AddTransient<IPrivateBookListSqlService, PrivateBookListSqlService>();
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddSingleton<IEntityUpdateService, EntityUpdateService>();
            services.AddDbContext<WriteDbContext>((provider, builder) =>
                builder.UseSqlServer(provider.GetDefaultConnectionString()));
            services.AddScoped<IDbConnection, SqlConnection>(provider =>
                new SqlConnection(provider.GetDefaultConnectionString()));
            services.AddScoped<IReadDbConnection, ReadDbConnection>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerConfigurator.Configure);
            Mapper.Initialize(conf =>
            {
                conf.AddProfile<BookListProfile>();
                conf.AddProfile<PrivateBookListItemProfile>();
                conf.AddProfile<UserProfile>();
            });
            return services;
        }
    }
}