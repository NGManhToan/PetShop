using Microsoft.AspNetCore.Mvc;
using PetShop.Models.PageList;
using PetShop.Models.Product;
using PetShop.Service.Interface;

namespace PetShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ListProduct(int pageNumber = 1, int pageSize = 20)
        {
            var products = await _productService.ListProduct(pageNumber, pageSize);
            var totalProducts = await _productService.GetTotalProducts();
            var model = new PagedListModel<ListProductModel>
            {
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
                Items = products
            };
            return View(model);
        }


    }
}

