﻿using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;
using Ordering.Application.Features.Orders.Queries.GetOrderList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapping
{
    public class MappingProfile : Profile
    {
        MappingProfile()
        {
            CreateMap<Order, OrderVm>().ReverseMap();
            CreateMap<CheckOutOrderCommand, Order>().ReverseMap();
        }
    }
}
