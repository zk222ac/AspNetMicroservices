using Concession.API.Entities;
using System.Threading.Tasks;

namespace Concession.API.Repositories
{
    public interface IConcessionRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
    }
}
