using Microsoft.AspNetCore.Mvc;
using PetShop.Action.Interface;
using PetShop.Models.Cart;
using PetShop.Service.Interface;

namespace PetShop.Controllers
{

    public class CartController : Controller
    {
        private readonly ICartAction _cartAction;
        private readonly ICartService _cartService;

        public CartController(ICartAction cartAction, ICartService cartService)
        {
            _cartAction = cartAction;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            // Retrieve user ID from the current user's claims
            var userId = HttpContext.Session.GetString("UserId");
            if(userId != null)
            {
                // Pass the user ID to get the user-specific cart
                var cart = await _cartService.GetListCart(int.Parse(userId));

                // Pass the cart to the view
                return View(cart);
            }
            return View();
            
        }



		[HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
        {
            var result = await _cartAction.AddToCart(cartItem);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Không thể thêm sản phẩm vào giỏ hàng");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItem cartItem)
        {
            var result = await _cartAction.UpdateToCart(cartItem);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Không thể cập nhật sản phẩm trong giỏ hàng");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem([FromBody] CartItem cartItem)
        {
            var result = await _cartAction.DeleteToCart(cartItem);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Không thể xóa sản phẩm khỏi giỏ hàng");
            }
        }
    }
}
