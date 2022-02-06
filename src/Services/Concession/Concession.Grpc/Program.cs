using Concession.Grpc.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Concession.Grpc
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

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
