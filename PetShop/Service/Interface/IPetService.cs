using PetShop.Models.Pet;

namespace PetShop.Service.Interface
{
    public interface IPetService
    {
        Task<List<PetListModel>> ListPet(int pageNumber, int pageSize);

        Task<List<PetListCategoryModel>> ListCategoryPet();

        Task<List<PetListModel>> FitterListByCategoryPet(int categoryId, int pageNumber, int pageSize);
        Task<DetailItemPetMModal> DetailItemPet(int petId);
    }
}
