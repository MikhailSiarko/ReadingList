using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ReadingList.WriteModel
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
    {
        public MigrationDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory()).GetDirectories()
                        .Single(d => d.Name == "ReadingList.Api").FullName,
                    "appsettings.json"));
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
            return new MigrationDbContext(optionsBuilder.Options);
        }
    }
}