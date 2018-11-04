using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Middlewares;
using ReadingList.Application;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Services;

namespace ReadingList.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            ConfigureApplication(services);
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

        private static void ConfigureApplication(IServiceCollection services)
        {
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddApplication();
        }
    }
}