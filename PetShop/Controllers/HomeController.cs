using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Models.PageList;
using PetShop.Models.Product;
using PetShop.Service.Interface;
using System.Diagnostics;

namespace PetShop.Controllers
{

	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productService.ListProduct(pageNumber, pageSize);
            var model = new PagedListModel<ListProductModel>
            {
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(products.Count / (double)pageSize),
                Items = products
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}