using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ReadingList.Write
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory()).GetDirectories()
                        .Single(d => d.Name == "ReadingList.Api").FullName,
                    "appsettings.json"));
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite(configuration.GetConnectionString("Write"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}