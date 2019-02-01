using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Infrastructure.Behaviors;
using ReadingList.Domain.Services;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection services)
        {
            services.AddTransient<IBookListService, BookListService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));

            AddValidatorsFromAssembly(services, typeof(JwtBearerConfigurator).Assembly);

            Mapper.Initialize(conf => conf.AddProfiles(typeof(DomainConfiguration).Assembly));

            return services;
        }

        public static void AddValidatorsFromAssembly(IServiceCollection services, Assembly assembly)
        {
            var scanner = AssemblyScanner.FindValidatorsInAssembly(assembly);

            foreach (var result in scanner)
            {
                services.AddTransient(result.InterfaceType, result.ValidatorType);
            }
        }
    }
}