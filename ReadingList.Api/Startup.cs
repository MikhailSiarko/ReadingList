using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Middlewares;
using ReadingList.Domain;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure;

namespace ReadingList.Api
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDomainModule();
            services.AddMvc().AddFluentValidation(options =>
            {
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                options.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(Startup)));
            });
            services.AddMediatR(typeof(ICommand).Assembly);
            var serviceProvider = services.BuildServiceProvider();
            MediatorContainer.InitializeMediator(serviceProvider);
            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>(InitializeMap());
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
    }
}