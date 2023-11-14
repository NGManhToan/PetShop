using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShop.Action.Interface;
using PetShop.Database;
using PetShop.Database.SharingModels;
using PetShop.Models.Cart;
using PetShop.Models.Product;
using PetShop.Models.UtilsProject;
using PetShop.Service.Interface;

namespace PetShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController( ICartService cartService)
        {
            _cartService = cartService;
        }


		[HttpGet("cart")]
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

        [HttpPost("noAccount")]
        public async Task<IActionResult> CheckoutNoAcc([FromBody] CheckoutRequestDto checkoutRequest)
        {
            try
            {
                await _cartService.CheckoutNoAcc(checkoutRequest);
                return Ok("Dữ liệu giỏ hàng đã được lưu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể lưu dữ liệu giỏ hàng vào cơ sở dữ liệu: " + ex.Message);
            }
        }


    }
}
