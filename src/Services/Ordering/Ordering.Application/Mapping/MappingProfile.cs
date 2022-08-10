using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrderList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderVm>().ReverseMap();
            CreateMap<CheckOutOrderCommand, Order>().ReverseMap();
            CreateMap<UpdateOrderCommand, Order>().ReverseMap();
        }
    }
}
