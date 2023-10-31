using Microsoft.EntityFrameworkCore;
using PetShop.Action.Interface;
using PetShop.Database;
using PetShop.Database.SharingModels;
using PetShop.Models.Cart;

namespace PetShop.Action
{
    public class CartAction : ICartAction
    {
        private readonly SharingContext _context;

        public CartAction(SharingContext context)
        {
            _context = context;
        }

        public async Task<TblCart> AddToCart(CartItem cart)
        {
            var product = cart.Product.FirstOrDefault();
            if (product != null)
            {
                var cartItem = await _context.TblCarts
                    .FirstOrDefaultAsync(c => c.ProductId == product.Id);

                if (cartItem != null)
                {
                    // Nếu sản phẩm đã có trong giỏ hàng, tăng số lượng
                    cartItem.Quantity += cart.Quantity; // Sửa ở đây
                }
                else
                {
                    // Nếu sản phẩm chưa có trong giỏ hàng, thêm mới với số lượng từ cart
                    var newCartItem = new TblCart
                    {
                        ProductId = product.Id,
                        Quantity = cart.Quantity // Sửa ở đây
                    };
                    _context.TblCarts.Add(newCartItem);
                }

                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            }

            return new TblCart(); // Cập nhật logic trả về nếu cần thiết
        }


        public async Task<TblCart> UpdateToCart(CartItem cartItem)
        {
            var updateItem = await _context.TblCarts.FindAsync(cartItem.ProductId);
            if (updateItem != null)
            {
                updateItem.Quantity = cartItem.Quantity;
                _context.TblCarts.Update(updateItem);
                await _context.SaveChangesAsync();

            }
            return new TblCart();
        }

        public async Task<bool> DeleteToCart(CartItem cartItem)
        {
            var deleteItem = await _context.TblCarts.FindAsync(cartItem.ProductId);
            if (deleteItem != null)
            {
                _context.TblCarts.Remove(deleteItem);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
