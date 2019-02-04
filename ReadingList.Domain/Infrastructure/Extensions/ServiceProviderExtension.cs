using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class ServiceProviderExtension
    {
        public static string GetConnectionString(this IServiceProvider provider, string name)
        {
            return provider.GetService<IConfiguration>().GetConnectionString(name);
        }
    }
}