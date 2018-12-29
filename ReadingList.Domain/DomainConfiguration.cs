using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Infrastructure.Behaviors;

namespace ReadingList.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            Mapper.Initialize(conf => conf.AddProfiles(typeof(DomainConfiguration).Assembly));

            return services;
        }
    }
}