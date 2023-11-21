using PetShop.Models.Cart;

namespace PetShop.Query.Interface
{
    public interface ICartQuery
    {
        Task<List<CartViewModelAcc>> GetListCart(int id);
        Task<CountItemModel> CountSLSP(string id);

    }
}
