using PetShop.Database.SharingModels;
using PetShop.Models.Cart;
using PetShop.Models.UtilsProject;

namespace PetShop.Action.Interface
{
    public interface ICartAction
    {
        Task CheckoutNoAcc(CheckoutRequestDto checkoutRequest);
        Task Checkout(CheckoutRequestDto checkoutRequest, string userId);

    }
}
