using PetShop.Models.PageList;

namespace PetShop.Models.Product
{
    public class AllListProduct
    {
        public List<ListProductModel> Products { get; set; }
        public List<ListCategoryProductModel> Categories { get; set; }
        public PagedListModel<ListProductModel> Pagination { get; set; }
    }

}
