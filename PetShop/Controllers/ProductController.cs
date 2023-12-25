using Microsoft.AspNetCore.Mvc;
using PetShop.Models.PageList;
using PetShop.Models.Product;
using PetShop.Service.Interface;
using System.Text.Json;

namespace PetShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

		[HttpGet("list")]
		public async Task<IActionResult> ListProduct(int pageNumber = 1, int pageSize = 15)
		{
			var json = TempData["FilteredProducts"] as string; // Retrieve the JSON string from TempData
			var products = json != null ? JsonSerializer.Deserialize<List<ListProductModel>>(json) : null; if (products == null) // If no filtered products, get the full list
			{
				products = await _productService.ListProduct(pageNumber, pageSize);
			}
			var categoryProduct = await _productService.ListCategoryProducts();
			var totalProducts = await _productService.GetTotalProducts();

			var model = new PagedListModel<ListProductModel>
			{
				CurrentPage = pageNumber,
				TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
				PageSize = pageSize,
				TotalItems = totalProducts,
				Items = products
			};

			var viewModel = new AllListProduct
			{
				Products = products,
				Categories = categoryProduct,
				Pagination = model
			};

			return View(viewModel);
		}


		[HttpGet("category/{categoryId}")]
		public async Task<IActionResult> FillterCategoryProduct(int categoryId, int pageNumber = 1, int pageSize = 15)
        {
            var result = await _productService.FitterListByCategoryProduct(categoryId, pageNumber, pageSize);


            var originalModel = new AllListProduct
            {
                Products = result,
                Categories = await _productService.ListCategoryProducts(),
                Pagination = new PagedListModel<ListProductModel>
                {

                }
            };

            return View("ListProduct", originalModel);

        }

		[HttpGet("detail/{productId}")]
		public async Task<IActionResult> DetailItem(int productId)
        {

            var result = await _productService.DetailItem(productId);

            return View("DetailItem", result);
        }

		

	}
}
