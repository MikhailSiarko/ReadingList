using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Middlewares;
using ReadingList.Application;
using ReadingList.Application.Authentication.AuthenticationOptions;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Services;
using ReadingList.Application.Services.Authentication;
using ReadingList.Application.Services.Encryption;

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
                options.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(JwtBearerConfigurator)));
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
                        typeof(ObjectAlreadyExistsException),
                        typeof(CannotChangeStatusException),
                        typeof(ObjectNotExistException),
                        typeof(ValidationException)
                    }
                },
                {
                    HttpStatusCode.NotFound,
                    new[]
                    {
                        typeof(ObjectNotFoundException)
                    }
                },
                {
                    HttpStatusCode.Forbidden,
                    new []
                    {
                        typeof(AccessDeniedException)
                    }
                }
            };
        }

        private static void ConfigureDomain(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddSingleton<IEntityUpdateService, EntityUpdateService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddApplicationModule();
        }
    }
}