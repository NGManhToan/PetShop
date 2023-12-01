using PetShop.Action.Interface;
using PetShop.Models.Cart;
using PetShop.Models.UtilsProject;
using PetShop.Query.Interface;
using PetShop.Service.Interface;

namespace PetShop.Service
{
    public class CartService : ICartService
    {
        private readonly ICartQuery _cartQuery;
        private readonly ICartAction _cartAction;

        public CartService(ICartQuery cartQuery, ICartAction cartAction)
        {
            _cartQuery = cartQuery; 
            _cartAction = cartAction;
        }

        public async Task<List<CartViewModelAcc>> GetListCart(int id)
        {
            return await _cartQuery.GetListCart(id);
        }

        public async Task CheckoutNoAcc(CheckoutRequestDto checkoutRequest)
        {
            if (checkoutRequest == null)
            {
                throw new ArgumentNullException(nameof(checkoutRequest));
            }

            await _cartAction.CheckoutNoAcc(checkoutRequest);
        }
        public async Task Checkout(CheckoutRequestDto checkoutRequest, string userId)
        {
            if (checkoutRequest == null)
            {
                throw new ArgumentNullException(nameof(checkoutRequest));
            }

            await _cartAction.Checkout(checkoutRequest, userId);
        }

        public async Task<CountItemModel> CountSLSP(string id)
        {
            var countItem = await _cartQuery.CountSLSP(id);
            if (countItem == null)
            {
                return null;
            }
            return countItem;
        }

        public async Task<List<OrderedCart>> GetListHistoryProduct(string id)
        {
            var historyProduct = await _cartQuery.GetListHistoryProduct(id);
            if(historyProduct == null)
            {
                return null;
            }
            return historyProduct;
        }
    }
}
