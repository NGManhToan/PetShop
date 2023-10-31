using PetShop.Database.SharingModels;
using PetShop.Models.Cart;

namespace PetShop.Action.Interface
{
    public interface ICartAction
    {
        Task<TblCart> AddToCart(CartItem cart);
        Task<TblCart> UpdateToCart(CartItem cartItem);
        Task<bool> DeleteToCart(CartItem cartItem);

    }
}
