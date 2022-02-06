using AutoMapper;
using Concession.Grpc.Entities;
using Concession.Grpc.Protos;

namespace Concession.Grpc.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }             
    }
}
