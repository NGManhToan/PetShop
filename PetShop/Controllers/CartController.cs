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
        private readonly ICartAction _cartAction;
        private readonly ICartService _cartService;
        private readonly SharingContext _context;

        public CartController(ICartAction cartAction, ICartService cartService,SharingContext sharingContext)
        {
            _cartAction = cartAction;
            _cartService = cartService;
            _context = sharingContext;
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
			var ramdomPass = RamdomPass.RandomPassword();
			try
			{
				// Truy cập dữ liệu từ checkoutRequest thay vì các biến global
				TblUser user = new TblUser
				{
					Email = checkoutRequest.InfoUser.Email,
					Password = Encryptor.SHA256Encode(ramdomPass),
					LastName = checkoutRequest.InfoUser.LastName,
					FirstName = checkoutRequest.InfoUser.FirstName,
				};

				TblCustomer customer = new TblCustomer
				{
                    UserId = user.UserId,
					CustomerName = user.FirstName + " " + user.LastName,
					CustomerContactInfo = checkoutRequest.InfoUser.customer_contact_info,
				};

				// Lưu thông tin người dùng và khách hàng vào cơ sở dữ liệu
				_context.TblUsers.Add(user);
				_context.TblCustomers.Add(customer);

				foreach (var item in checkoutRequest.CartItems)
				{
					TblOrder tblOrder = new TblOrder
					{
						TotalAmount = item.Price,
                        CustomerId = customer.CustomerId,
                        CreatedBy = customer.CustomerId,
                        OrderDate = Utils.DateNow(),
                        OrderStatus = "Đã đặt",
                        IsActive = 1,
                        IsDeleted = 0,
                        LastModifiedBy = customer.CustomerId,

					};

					TblOrderDetail tblOrderDetail = new TblOrderDetail
					{
                        OrderId = tblOrder.OrderId,
						Quantity = item.Quantity,
						ProductId = item.Id,
                        CreatedBy = tblOrder.CreatedBy,
                        IsActive = 1,
                        IsDeleted = 0,
                        LastModifiedBy = tblOrder.CreatedBy,
                    };

					_context.TblOrders.Add(tblOrder);
					_context.TblOrderDetails.Add(tblOrderDetail);
				}

				await _context.SaveChangesAsync();

				return Ok("Dữ liệu giỏ hàng đã được lưu.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Không thể lưu dữ liệu giỏ hàng vào cơ sở dữ liệu: " + ex.Message);
			}
		}


		[HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] List<CartViewModel> cartItems)
        {
            

            

            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest("Cart is empty.");
            }

            try
            {
                foreach (var item in cartItems)
                {
                    TblOrder tblOrder = new TblOrder
                    {
                        TotalAmount = item.Price,
                        
                    };

                    TblOrderDetail tblOrderDetail = new TblOrderDetail
                    {
                        Quantity = item.Quantity,
                        ProductId = item.Id,
                    };

                    _context.TblOrders.Add(tblOrder);
                    _context.TblOrderDetails.Add(tblOrderDetail);
                }

                await _context.SaveChangesAsync(); // Save changes to the database

                return Ok("Cart data has been saved.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unable to save cart data to the database: " + ex.Message);
            }
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



        //    [HttpPost]
        //    public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
        //    {
        //        var result = await _cartAction.AddToCart(cartItem);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //        else
        //        {
        //            return BadRequest("Không thể thêm sản phẩm vào giỏ hàng");
        //        }
        //    }


        //    [HttpPut]
        //    public async Task<IActionResult> UpdateCartItem([FromBody] CartItem cartItem)
        //    {
        //        var result = await _cartAction.UpdateToCart(cartItem);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //        else
        //        {
        //            return BadRequest("Không thể cập nhật sản phẩm trong giỏ hàng");
        //        }
        //    }

        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteCartItem([FromBody] CartItem cartItem)
        //    {
        //        var result = await _cartAction.DeleteToCart(cartItem);
        //        if (result)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return BadRequest("Không thể xóa sản phẩm khỏi giỏ hàng");
        //        }
        //    }
    }
}
