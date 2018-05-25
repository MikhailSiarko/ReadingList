using System;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain;
using ReadingList.Domain.Infrastructure.Helpers;

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
            services.AddMediatR(typeof(AsyncHelpers).Assembly);
            var serviceProvider = services.BuildServiceProvider();
            MediatorContainer.InitializeMediator(serviceProvider);
            return serviceProvider;
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseMvc();
        }   
    }
}