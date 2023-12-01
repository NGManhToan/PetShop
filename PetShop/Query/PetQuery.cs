using PetShop.Models.Pet;
using PetShop.Query.Interface;
using PetShop.UtilsService.Interface;

namespace PetShop.Query
{
    public class PetQuery : IPetQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public PetQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }

        public async Task<List<PetListModel>> ListPet(int pageNumber, int pageSize)
        {
            var query = @"SELECT 
                                pe.pet_id as ID,
								pe.pet_name AS PetName,
								pe.pet_price AS PetPrice,
								m.Image_media AS PetImage
							FROM
								tbl_pet pe
                                join tbl_media m on m.pet_id = pe.pet_id
							WHERE
								pe.IsActive = 1 AND pe.IsDeleted = 0
							ORDER BY pe.pet_name";
            return await _sharingDapper.QueryAsync<PetListModel>(query);
        }

        public async Task<List<PetListCategoryModel>> ListCategoryPet()
        {
            var query = @"SELECT pec.pet_category_id as PetCategoryId, pec.pet_category_name as PetCategoryName
                          FROM tbl_pet_category pec";
            return await _sharingDapper.QueryAsync<PetListCategoryModel>(query);
        }
        public async Task<List<PetListModel>> FitterListByCategoryPet(int categoryId, int pageNumber, int pageSize)
        {
            var query = @"SELECT 
                                pe.pet_id as ID,
								pe.pet_name AS PetName,
								pe.pet_price AS PetPrice,
								m.Image_media AS PetImage
							FROM
								tbl_pet pe
                                join tbl_media m on m.pet_id = pe.pet_id
							WHERE
								pe.IsActive = 1 AND pe.IsDeleted = 0 And pe.pet_category_id=@categoryId
						ORDER BY pe.pet_name
                          LIMIT @PageSize OFFSET @Offset";

            var parameters = new { CategoryId = categoryId, PageSize = pageSize, Offset = (pageNumber - 1) * pageSize };
            return await _sharingDapper.QueryAsync<PetListModel>(query, parameters);
        }
    }
}
