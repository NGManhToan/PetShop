using Microsoft.AspNetCore.Mvc;

namespace PetShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
