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
                .UseStartup<Startup>()
                .Build();

        private static void ConfigureClientProcess()
        {
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = Environment.OSVersion.Platform == PlatformID.Win32NT
                        ? Constants.CommandLine.Windows
                        : Constants.CommandLine.Unix,
                    WorkingDirectory = Path.Combine(
                        Directory.GetParent(Directory.GetCurrentDirectory())
                            .GetDirectories()
                            .Single(x => x.Name == "ReadingList.Client").FullName),
                    Arguments = (Environment.OSVersion.Platform == PlatformID.Win32NT
                        ? Constants.CommandFlag.Windows
                        : Constants.CommandFlag.Unix) + " npm run start"
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
    
    public static class Constants
    {
        public static class CommandLine
        {
            public const string Windows = "cmd";

            public const string Unix = "#!/usr/bin/env bash";
        }
        
        public static class CommandFlag
        {
            public const string Windows = "/c";

            public const string Unix = "-c";
        }
    }
}