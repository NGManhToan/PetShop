using PetShop.Models.Cart;

namespace PetShop.Query.Interface
{
    public interface ICartQuery
    {
        Task<List<CartViewModel>> GetListCart(int id);

    }
}
