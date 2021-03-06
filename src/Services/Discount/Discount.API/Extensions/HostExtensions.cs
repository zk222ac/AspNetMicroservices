using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Threading;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrationDatabase<TContext>(this IHost host, int? retry = 0) 
        {
            int retryForAvaliability = retry.Value;
            using(var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var configuration = service.GetRequiredService<IConfiguration>();
                var logger = service.GetRequiredService<ILogger<TContext>>();
                try 
                {
                    logger.LogInformation("Migrating postresql database:");
                    using var connection = new NpgsqlConnection
                        (configuration.GetValue<string>("DatabaseSettings:ConnectionSetttings"));
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    // Drop table if same name table coupon exist .......................

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    // Create Table coupon ...............................
                    command.CommandText = @"CREATE TABLE Coupon (Id SERIAL PRIMARY KEY,
                                                                 ProductName VARCHAR(24) NOT NULL,
                                                                 Description TEXT,
                                                                 Amount INT)";
                    command.ExecuteNonQuery();

                    // Insert some values
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description,Amount) Values ('IPhone X', 'IPhone discount', 150)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description,Amount) Values ('Samsung 10', 'Samsung discount', 100)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postresql database");

                }
                catch (NpgsqlException ex) 
                {
                    logger.LogError(ex,"An error occurred while migrating the postresql database");
                    if(retryForAvaliability < 50) 
                    {
                        retryForAvaliability++;
                        Thread.Sleep(2000);
                        MigrationDatabase<TContext>(host, retryForAvaliability);
                    }
                }                
            }
            return host;
        } 
    }
}
