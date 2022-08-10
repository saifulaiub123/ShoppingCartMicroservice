using AutoMapper;
using EventBus.Message.Events;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;

namespace Ordering.Api.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<BasketCheckoutEvent, CheckOutOrderCommand>().ReverseMap();
        }
    }
}
