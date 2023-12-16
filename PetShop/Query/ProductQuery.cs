using PetShop.Models.Product;
using PetShop.Query.Interface;
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
            var query = @"SELECT
						  p.product_id as Id,
                          p.product_name AS ProductName,
                          p.product_price AS ProductPrice,
						  m.Image_media AS ProductImageUrl,
                          p.discount as Discount
                          FROM tbl_product p
							join tbl_media m on m.product_id = p.product_id
                          WHERE p.IsActive = 1 AND p.IsDeleted = 0
                          ORDER BY p.product_name
                          LIMIT @PageSize OFFSET @Offset";

            var parameters = new { PageSize = pageSize, Offset = (pageNumber - 1) * pageSize };
            var ListProduct = await _sharingDapper.QueryAsync<ListProductModel>(query, parameters);
            return ListProduct;

        }

        public async Task<List<ListCategoryProductModel>> ListCategoryProducts()
        {
            var query = @"SELECT pc.category_id as CategoryId, pc.category_name as CategoryName
                          FROM tbl_product_category pc";
            return await _sharingDapper.QueryAsync<ListCategoryProductModel>(query);
        }

        public async Task<List<ListProductModel>> FitterListByCategoryProduct(int categoryId, int pageNumber, int pageSize)
        {
            var query = @"SELECT
                          p.product_name AS ProductName,
                          p.product_price AS ProductPrice,
                          m.Image_media AS ProductImageUrl,
                          p.discount as Discount 
                          FROM tbl_product p
                          join tbl_media m on m.product_id = p.product_id
                          WHERE p.IsActive = 1 AND p.IsDeleted = 0 AND p.category_id = @categoryId
                          ORDER BY p.product_name
                          LIMIT @PageSize OFFSET @Offset";

            var parameters = new { CategoryId = categoryId, PageSize = pageSize, Offset = (pageNumber - 1) * pageSize };
            return await _sharingDapper.QueryAsync<ListProductModel>(query, parameters);
        }

        public async Task<DetailItemModel> DetailItem(int productId)
        {
            var query = @"SELECT 
								p.product_id as Id,
								p.product_name AS ProductName,
								p.product_description AS DescriptionProduct,
								p.product_price as Price,
								m.Image_media AS ProductImageUrl,
								pc.category_name as CategoryName
							FROM
								tbl_product p
									JOIN
								tbl_product_category pc ON pc.category_id = p.category_id
                                    join tbl_media m on m.product_id = p.product_id
							WHERE
								p.IsDeleted = 0 AND p.IsActive = 1
									AND p.product_id = @productId";

            var detail = await _sharingDapper.QuerySingleAsync<DetailItemModel>(query, new
            {
                productId = productId
            });
            return detail;
        }
    }
}
