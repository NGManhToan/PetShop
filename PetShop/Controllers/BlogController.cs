using Microsoft.AspNetCore.Mvc;

namespace PetShop.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult BlogGrid()
        {
            return View();
        }

        public IActionResult DetailPage()
        {
            return View();
        }

    }
}
