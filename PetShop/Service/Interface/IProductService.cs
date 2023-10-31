using PetShop.Models.Product;

namespace PetShop.Service.Interface
{
    public interface IProductService
    {
        Task<List<ListProductModel>> ListProduct(int pageNumber, int pageSize);
        Task<int> GetTotalProducts();
        Task<List<ListCategoryProductModel>> ListCategoryProducts();

        Task<List<ListProductModel>> FitterListByCategoryProduct(int categoryId, int pageNumber, int pageSize);
        Task<DetailItemModel> DetailItem(int productId);
    }
}
