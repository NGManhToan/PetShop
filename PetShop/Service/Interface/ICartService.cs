using PetShop.Models.Cart;

namespace PetShop.Service.Interface
{
    public interface ICartService
    {
        Task<List<CartViewModelAcc>> GetListCart(int id);
        Task CheckoutNoAcc(CheckoutRequestDto checkoutRequest);

        Task Checkout(CheckoutRequestDto checkoutRequest, string userId);
        Task<CountItemModel> CountSLSP(string id);

        Task<List<OrderedCart>> GetListHistoryProduct(string id);

        Task CancelItem(OrderedCart orderedCart, string userId);
    }
}
