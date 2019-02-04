using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Read.SqlQueries;

namespace ReadingList.Read
{
    public static class ReadConfiguration
    {
        public static IServiceCollection RegisterReadDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDbConnection, SqliteConnection>(provider =>
                new SqliteConnection(provider.GetConnectionString("Read")));

            DomainConfiguration.AddValidatorsFromAssembly(services, typeof(ListsQueries).Assembly);

            return services;
        }
    }
}