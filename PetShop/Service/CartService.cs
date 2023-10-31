using PetShop.Models.Cart;
using PetShop.Query.Interface;
using PetShop.Service.Interface;

namespace PetShop.Service
{
    public class CartService : ICartService
    {
        private readonly ICartQuery _cartQuery;

        public CartService(ICartQuery cartQuery)
        {
            _cartQuery = cartQuery;
        }

        public async Task<List<CartViewModel>> GetListCart(int id)
        {
            return await _cartQuery.GetListCart(id);
        }
    }
}
