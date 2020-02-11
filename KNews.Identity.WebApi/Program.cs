using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KNews.Identity.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((host, cfg) =>
                {
                    cfg.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false);
                    cfg.AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true);
                })
                .UseSerilog((context, log) => { log.ReadFrom.Configuration(context.Configuration); });
    }
}
