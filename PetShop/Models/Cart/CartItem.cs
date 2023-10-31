using PetShop.Models.Product;

namespace PetShop.Models.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; } // Thêm thuộc tính này để đại diện cho ProductId của sản phẩm
        public List<ListProductModel> Product { get; set; }
        public int Quantity { get; set; }
    }
}
