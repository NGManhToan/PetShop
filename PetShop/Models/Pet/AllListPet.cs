using PetShop.Models.PageList;

namespace PetShop.Models.Pet
{
    public class AllListPet
    {
        public List<PetListModel> Pet { get; set; }

        public List<PetListCategoryModel> Categories { get; set; }
        public PagedListModel<PetListModel> Pagination { get; set; }
    }
}
