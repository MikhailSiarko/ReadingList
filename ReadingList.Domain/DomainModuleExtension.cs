using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Api.Authentication.AuthenticationOptions;
using ReadingList.Domain.Services.Authentication;
using ReadingList.ReadModel;
using ReadingList.WriteModel;

namespace ReadingList.Domain
{
    public static class DomainModuleExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddDbContext<ReadingListDbContext>();
            services.AddScoped<ReadingListConnection>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerConfigurator.Configure);
            return services;
        }
    }
}