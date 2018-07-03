using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class ServiceProviderExtension
    {
        public static string GetDefaultConnectionString(this IServiceProvider provider)
        {
            return provider.GetService<IConfiguration>().GetConnectionString("Default");
        }

        public static string GetConnectionString(this IServiceProvider provider, string name)
        {
            return provider.GetService<IConfiguration>().GetConnectionString(name);
        }
    }
}
