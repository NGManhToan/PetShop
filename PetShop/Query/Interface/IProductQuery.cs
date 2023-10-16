using PetShop.Database.SharingModels;
using PetShop.Models.Product;

namespace PetShop.Query.Interface
{
    public interface IProductQuery
    {
        Task<List<ListProductModel>> ListProduct(int pageNumber, int pageSize);
        Task<int> GetTotalProducts();
    }
}
