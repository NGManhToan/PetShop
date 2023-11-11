namespace PetShop.Models.Cart
{
    public class CheckoutRequestDto
    {
        public List<CartViewModel> CartItems { get; set; }
        public InfoUserCartModel InfoUser { get; set; }
    }
}
