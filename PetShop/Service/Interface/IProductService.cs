using PetShop.Models.Product;

namespace PetShop.Service.Interface
{
    public interface IProductService
    {
        Task<List<ListProductModel>> ListProduct(int pageNumber, int pageSize);
        Task<int> GetTotalProducts();
    }
}
