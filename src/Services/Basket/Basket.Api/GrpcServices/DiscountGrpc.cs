using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }
        public async Task<CouponModel> GetDiscount(string productName)
        {
            return await _grpcClient.GetDiscountAsync(new GetDiscountRequest() { ProductName = productName });
        }
    }
}
