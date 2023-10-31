using PetShop.Models.Product;

namespace PetShop.Query.Interface
{
    public interface IProductQuery
    {
        Task<List<ListProductModel>> ListProduct(int pageNumber, int pageSize);
        Task<int> GetTotalProducts();

        Task<List<ListCategoryProductModel>> ListCategoryProducts();

        Task<List<ListProductModel>> FitterListByCategoryProduct(int categoryId, int pageNumber, int pageSize);

        Task<DetailItemModel> DetailItem(int productId);

    }
}
