using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Infrastructure.Extensions;

namespace ReadingList.Read
{
    public static class ReadConfiguration
    {
        public static IServiceCollection RegisterReadDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDbConnection, SqliteConnection>(provider =>
                new SqliteConnection(provider.GetConnectionString("Read")));

            return services;
        }
    }
}