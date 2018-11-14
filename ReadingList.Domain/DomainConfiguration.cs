using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Infrastructure.Behaviors;
using ReadingList.Domain.MapperProfiles;

namespace ReadingList.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            Mapper.Initialize(conf =>
            {
                conf.AddProfile<PrivateBookListProfile>();
                conf.AddProfile<SharedBookListProfile>();
                conf.AddProfile<UserProfile>();
            });

            return services;
        }
    }
}