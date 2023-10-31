using PetShop.Models.Cart;

namespace PetShop.Service.Interface
{
    public interface ICartService
    {
        Task<List<CartViewModel>> GetListCart(int id);
    }
}
