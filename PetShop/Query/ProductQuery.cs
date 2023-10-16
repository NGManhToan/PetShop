using PetShop.Database.SharingModels;
using PetShop.Models.Product;
using PetShop.Query.Interface;
using PetShop.UtilsService;
using PetShop.UtilsService.Interface;

namespace PetShop.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ISharingDapper _sharingDapper;

        public ProductQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }


        public async Task<int> GetTotalProducts()
        {
            var query = @"SELECT COUNT(*)
                  FROM tbl_product p
                  WHERE p.IsActive = 1 AND p.IsDeleted = 0";
            return await _sharingDapper.QuerySingleAsync<int>(query);
        }
        public async Task<List<ListProductModel>> ListProduct(int pageNumber, int pageSize)
        {
            var query = @"SELECT distinct
                        p.product_name AS ProductName,
                        p.product_price AS ProductPrice,
                        p.product_image_url AS ProductImageUrl
                  FROM
                        tbl_product p
                  WHERE p.IsActive = 1 AND p.IsDeleted = 0
                  ORDER BY p.product_name
                  LIMIT @PageSize OFFSET @PageNumber";
            var parameters = new { PageNumber = (pageNumber - 1) * pageSize, PageSize = pageSize };
            return await _sharingDapper.QueryAsync<ListProductModel>(query, parameters);
        }


    }
}
