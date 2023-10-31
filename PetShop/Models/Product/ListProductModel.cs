namespace PetShop.Models.Product
{
    public class ListProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal? Discount { get; set; }
    }
}
