using System;
using System.Collections.Generic;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Middlewares;
using ReadingList.Domain;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services;
using ReadingList.Domain.Services.Authentication;
using ReadingList.Domain.Services.Encryption;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Read;
using ReadingList.Write;

namespace ReadingList.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            ConfigureApplication(services);
            services.AddMvc();
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
                        typeof(DomainValidationException)
                    }
                },
                {
                    HttpStatusCode.Forbidden,
                    new[]
                    {
                        typeof(AccessDeniedException)
                    }
                },
                {
                    HttpStatusCode.UnprocessableEntity,
                    new[]
                    {
                        typeof(ObjectAlreadyExistsException),
                        typeof(CannotChangeStatusException),
                        typeof(WrongPasswordException),
                        typeof(ObjectNotExistException)
                    }
                }
            };
        }

        private static void ConfigureApplication(IServiceCollection services)
        {
            services.AddScoped<IDomainService, DomainService>();

            services.RegisterDomainDependencies();
            services.RegisterReadDependencies();
            services.RegisterWriteDependencies();

            services.AddMediatR(typeof(SecuredCommand).Assembly, typeof(SqlQueryContext<,>).Assembly);

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerConfigurator.Configure);
        }
    }
}