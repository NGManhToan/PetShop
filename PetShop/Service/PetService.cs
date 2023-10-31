using PetShop.Models.Pet;
using PetShop.Query.Interface;
using PetShop.Service.Interface;

namespace PetShop.Service
{
    public class PetService : IPetService
    {
        private readonly IPetQuery _petQuery;

        public PetService(IPetQuery petQuery)
        {
            _petQuery = petQuery;
        }

        public async Task<List<PetListModel>> ListPet(int pageNumber, int pageSize)
        {
            return await _petQuery.ListPet(pageNumber, pageSize);
        }

        public async Task<List<PetListCategoryModel>> ListCategoryPet()
        {
            return await _petQuery.ListCategoryPet();
        }

        public async Task<List<PetListModel>> FitterListByCategoryPet(int categoryId, int pageNumber, int pageSize)
        {
            return await _petQuery.FitterListByCategoryPet(categoryId, pageNumber, pageSize);
        }
    }
}
