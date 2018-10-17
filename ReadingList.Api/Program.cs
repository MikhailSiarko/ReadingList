using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReadingList.WriteModel;

namespace ReadingList.Api
{
    public class Program
    {
        private static Process _process;
        private static bool _isDevelopment;
        
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            _isDevelopment = host.Services.GetRequiredService<IHostingEnvironment>().IsDevelopment();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = host.Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            if(_isDevelopment)
                LaunchClient();
            
            host.Run();
            
            if(_isDevelopment)
                StopClient();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseShutdownTimeout(TimeSpan.FromSeconds(50))
                .Build();

        private static void ConfigureClientProcess()
        {
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = "cmd.exe",
                    WorkingDirectory = Path.Combine(
                        Directory.GetParent(Directory.GetCurrentDirectory())
                            .GetDirectories()
                            .Single(x => x.Name == "ReadingList.Client").FullName),
                    Arguments = "/c npm run start"
                }
            };
        }

        private static void LaunchClient()
        {
            ConfigureClientProcess();
            
            _process.Start();
        }

        private static void StopClient()
        {
            _process.CloseMainWindow();
            _process.Close();
        }
    }
}