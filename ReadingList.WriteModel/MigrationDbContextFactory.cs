using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ReadingList.WriteModel.Infrastructure;

namespace ReadingList.WriteModel
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
    {
        public WriteDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory()).GetDirectories()
                        .Single(d => d.Name == "ReadingList.Api").FullName,
                    "appsettings.json"));
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<WriteDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Write"));
            return new WriteDbContext(optionsBuilder.Options);
        }
    }
}