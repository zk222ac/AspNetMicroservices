using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Discount.API.Extensions;
namespace Discount.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Default code ...........
            //CreateHostBuilder(args)
            //    .Build()
            //    .Run();

            // call a extension method for seeding data 
            var host = CreateHostBuilder(args).Build();
            host.MigrationDatabase<Program>();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
