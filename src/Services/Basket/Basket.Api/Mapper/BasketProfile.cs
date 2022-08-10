using AutoMapper;
using Basket.Api.Entities;
using EventBus.Message.Events;

namespace Basket.Api.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckoutEvent, BasketCheckout>().ReverseMap();
        }
    }
}
