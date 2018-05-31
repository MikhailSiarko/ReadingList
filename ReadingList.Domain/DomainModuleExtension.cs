using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Authentication.AuthenticationOptions;
using ReadingList.Domain.MapperProfiles;
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
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddDbContext<WriteDbContext>();
            services.AddScoped<IDbConnection, SqlConnection>(provider =>
                new SqlConnection(provider.GetService<IConfiguration>().GetConnectionString("Default")));
            services.AddScoped<IReadDbConnection, ReadDbConnection>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerConfigurator.Configure);
            Mapper.Initialize(conf =>
            {
                conf.AddProfile<BookListProfile>();
            });
            return services;
        }
    }
}