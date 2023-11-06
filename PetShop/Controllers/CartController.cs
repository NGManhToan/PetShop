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

    public class CartController : Controller
    {
        private readonly ICartAction _cartAction;
        private readonly ICartService _cartService;
        private readonly SharingContext _context;

        public CartController(ICartAction cartAction, ICartService cartService,SharingContext sharingContext)
        {
            _cartAction = cartAction;
            _cartService = cartService;
            _context = sharingContext;
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


        public async Task<IActionResult> SaveLocalStorage()
        {
            // Retrieve user ID from the current user's claims
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                // Pass the user ID to get the user-specific cart
                var cart = await _cartService.GetListCart(int.Parse(userId));

                // Pass the cart to the view
                return View(cart);
            }
            return View();

        }
        //[HttpPost]
        //public IActionResult SaveOrderToDatabase([FromBody] OrderModel order)
        //{
        //    var userId = HttpContext.Session.GetString("UserId");
        //    if (order != null)
        //    {
        //        TblOrder tblOrder = new TblOrder
        //        {
        //            CustomerId = int.Parse(userId),
        //            CreatedDate = Utils.DateNow(),
        //            LastModifiedDate = Utils.DateNow(),
        //            OrderId = order.Id
        //        };
        //        TblOrderDetail tblOrderDetail = new TblOrderDetail
        //        {
        //            Quantity = order.Quantity,
        //            ProductId = order.ProductId,

        //        };
        //        _context.TblOrders.Add(tblOrder);
        //        _context.TblOrderDetails.Add(tblOrderDetail);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    return BadRequest("Không có dữ liệu đơn hàng để lưu.");
        //}



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
