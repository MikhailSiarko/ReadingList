using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Authentication.AuthenticationOptions;
using ReadingList.Domain.MapperProfiles;
using ReadingList.Domain.Services.Authentication;
using ReadingList.Domain.Services.Encryption;
using ReadingList.ReadModel;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel;

namespace ReadingList.Domain
{
    public static class DomainModuleExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddDbContext<WriteDbContext>();
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