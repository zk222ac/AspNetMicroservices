using Concession.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;
using Dapper;

namespace Concession.API.Repositories
{
    public class ConcessionRepository : IConcessionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection _connection;

        public ConcessionRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            // Insert New row
            var affected =
                await _connection.ExecuteAsync
                ("INSERT INTO Coupon(ProductName,Description,Amount) VALUES (@ProductName,@Description,@Amount)", 
                 new { ProductName=coupon.ProductName, Description=coupon.Description,Amount=coupon.Amount });
           
            if (affected == 0) 
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var affected =
               await _connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName ", new { ProductName = productName});

            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
         
            // Get the coupon of specific product
            var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>
             ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new {ProductName = productName});

            // return null if has no coupon
            if(coupon == null) 
            {
                return new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No discount avaliable" };
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            // Update existing Row 
            var affected =
                await _connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName = @ProductName,Description=@Description,Amount=@Amount WHERE Id=@Id",
                 new { ProductName=coupon.ProductName,Description=coupon.Description,Amount=coupon.Amount,Id=coupon.Id});

            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
