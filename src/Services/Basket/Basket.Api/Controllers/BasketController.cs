using Basket.Api.Entities;
using Basket.Api.Repositories;
using Basket.Api.GrpcServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpc;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpc)
        {
            _basketRepository = basketRepository;
            _discountGrpc = discountGrpc;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var cart = await _basketRepository.GetBasket(userName);
            return Ok(cart ?? new ShoppingCart(userName));

        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            foreach(var item in basket.Items)
            {
                var coupon = await _discountGrpc.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return Ok(await _basketRepository.UpdateBasket(basket));
        }
        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
