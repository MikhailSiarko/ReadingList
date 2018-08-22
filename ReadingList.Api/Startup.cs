using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Authentication.AuthenticationOptions;
using ReadingList.Api.Middlewares;
using ReadingList.Domain;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services;
using ReadingList.Domain.Services.Authentication;
using ReadingList.Domain.Services.Encryption;
using ReadingList.Domain.Services.Sql;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.WriteModel.Models;

namespace ReadingList.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            ConfigureDomain(services);
            services.AddMvc().AddFluentValidation(options =>
            {
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                options.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(Startup)));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>(new ExceptionToStatusCodeProvider(InitializeMap()));
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseMvc();
        }

        private static Dictionary<HttpStatusCode, Type[]> InitializeMap()
        {
            return new Dictionary<HttpStatusCode, Type[]>
            {
                {
                    HttpStatusCode.BadRequest,
                    new[]
                    {
                        typeof(WrongPasswordException),
                        typeof(UserAlreadyExistsException),
                        typeof(CannotChangeStatusException),
                        typeof(ObjectNotExistException)
                    }
                },
                {
                    HttpStatusCode.NotFound,
                    new[]
                    {
                        typeof(ObjectNotFoundException)
                    }
                }
            };
        }

        private static void ConfigureDomain(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IUserSqlService, UserSqlService>();
            services.AddTransient<PrivateBookListSqlService>();
            services.AddTransient<SharedBookListSqlService>();
            services.AddTransient<Func<BookListType, IBookListSqlService>>(serviceProvider => bookType =>
            {
                switch (bookType)
                {
                    case BookListType.Private:
                        return serviceProvider.GetService<PrivateBookListSqlService>();
                    case BookListType.Shared:
                        return serviceProvider.GetService<SharedBookListSqlService>();
                    default:
                        throw new ArgumentOutOfRangeException(nameof(bookType), bookType, null);
                }
            });
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddSingleton<IEntityUpdateService, EntityUpdateService>();
            services.AddScoped<IDomainService, DomainService>();
            services.AddDomainModule();
        }
    }
}