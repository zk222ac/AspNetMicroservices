using Concession.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concession.API
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
