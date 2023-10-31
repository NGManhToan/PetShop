using PetShop.Models.Cart;
using PetShop.Query.Interface;
using PetShop.UtilsService.Interface;

namespace PetShop.Query
{
    public class CartQuery : ICartQuery
    {
        private readonly ISharingDapper _sharingDapper;

        public CartQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }

        public async Task<List<CartViewModel>> GetListCart(int id)
        {
            var query = @"Select p.product_id as Id,p.product_image_url as Image, p.product_name Name ,product_price Price ,c.quantity Quantity
							from tbl_cart c
							join tbl_product p on p.product_id = c.product_id
							left join tbl_user u on u.user_id = c.user_id
							WHERE
								p.IsDeleted = 0 AND p.IsActive = 1 and u.user_id = @id";

            var cart = await _sharingDapper.QueryAsync<CartViewModel>(query, new
            {
                id = id
            });
            return cart;
        }
    }
}
