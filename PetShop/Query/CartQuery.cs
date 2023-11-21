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

        public async Task<List<CartViewModelAcc>> GetListCart(int id)
        {
            var query = @"Select cd.Image,cd.Name,cd.quanlity as Quantity,cd.total as Price, cd.product_id as IdProduct
							from tbl_cart c
							join tbl_user u on u.user_id = c.user_id
							join tbl_cart_detail cd on cd.cart_id = c.cart_id
							WHERE
								 c.user_id = @id and c.IsActive = 1 and c.IsDeleted = 0";

            var cart = await _sharingDapper.QueryAsync<CartViewModelAcc>(query, new
            {
                id = id
            });
            return cart;
        }

        public async Task<CountItemModel>CountSLSP(string id)
        {
            var query = @"Select count(cart_id) SLSP
                            from tbl_cart c
                            where c.user_id = @id and c.IsActive = 1 AND c.IsDeleted = 0";
            var count = await _sharingDapper.QuerySingleAsync<CountItemModel>(query, new
            {
                id = id
            });
            return count;
        }
    }
}
