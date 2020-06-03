using Dailylight.Server.Core;
using Dna;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Dailylight.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseDnaFramework(construct =>
                {
                    // Configure Dna Framework

                    // Add file logger
                    construct.AddFileLogger();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
