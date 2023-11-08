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
            var query = @"Select cd.Image,cd.Name,cd.quanlity as Quantity,cd.total as Price
							from tbl_cart c
							join tbl_user u on u.user_id = c.user_id
							join tbl_cart_detail cd on cd.cart_id = c.cart_id
							WHERE
								 c.user_id = @id";

            var cart = await _sharingDapper.QueryAsync<CartViewModel>(query, new
            {
                id = id
            });
            return cart;
        }
    }
}
