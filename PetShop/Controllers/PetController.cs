using Microsoft.AspNetCore.Mvc;
using PetShop.Models.PageList;
using PetShop.Models.Pet;
using PetShop.Service.Interface;

namespace PetShop.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        private readonly IProductService _productService;

        public PetController(IPetService petService, IProductService productService)
        {
            _petService = petService;
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> ListPet(int pageNumber = 1, int pageSize = 15)
        {
            var pet = await _petService.ListPet(pageNumber, pageSize);
            var petCategory = await _petService.ListCategoryPet();
            var totalProducts = await _productService.GetTotalProducts();

            var model = new PagedListModel<PetListModel>
            {
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
                PageSize = pageSize,
                TotalItems = totalProducts,
                Items = pet
            };

            var viewModel = new AllListPet
            {
                Pet = pet,
                Categories = petCategory,
                Pagination = model
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> FillterCategoryPet(int categoryId, int pageNumber = 1, int pageSize = 15)
        {
            var result = await _petService.FitterListByCategoryPet(categoryId, pageNumber, pageSize);


            var originalModel = new AllListPet
            {
                Pet = result,
                Categories = await _petService.ListCategoryPet(),
                Pagination = new PagedListModel<PetListModel>
                {

                }
            };

            return View("ListPet", originalModel);

        }

        [HttpGet("detail/{petId}")]
        public async Task<IActionResult> DetailItemPet(int petId)
        {

            var result = await _petService.DetailItemPet(petId);

            return View("DetailItemPet", result);
        }
    }
}
