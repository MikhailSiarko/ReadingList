using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Services.Authentication;
using ReadingList.WriteModel;

namespace ReadingList.Domain
{
    public static class DomainModuleExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddDbContext<ReadingListDbContext>();
            return services;
        }
    }
}