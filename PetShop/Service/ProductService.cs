using PetShop.Models.Product;
using PetShop.Query.Interface;
using PetShop.Service.Interface;

namespace PetShop.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductQuery _productQuery;

        public ProductService(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<List<ListProductModel>> ListProduct(int pageNumber, int pageSize)
        {
            return await _productQuery.ListProduct(pageNumber, pageSize);
        }

        public async Task<int> GetTotalProducts()
        {
            return await _productQuery.GetTotalProducts();
        }

    }
}
