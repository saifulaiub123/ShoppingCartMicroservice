using AutoMapper;
using Ordering.Application.Features.Order.Queries.GetOrderList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapping
{
    public class MappingProfile : Profile
    {
        MappingProfile()
        {
            CreateMap<Order, OrderVm>().ReverseMap();
        }
    }
}
