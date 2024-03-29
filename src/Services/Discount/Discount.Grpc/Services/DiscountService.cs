﻿using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if(coupon == null)
                 throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
            
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var result = await _discountRepository.CreateDiscount(_mapper.Map<Coupon>(request.Coupon));
            if (result)
                return request.Coupon;
            throw new RpcException(new Status(StatusCode.Internal, "Something went wrong"));
        }
        public override async Task<CouponModel> UpdateeDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _discountRepository.UpdateDiscount(coupon);
            _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            return new DeleteDiscountResponse()
            {
                Success = deleted
            }; ;
        }
    }
}
