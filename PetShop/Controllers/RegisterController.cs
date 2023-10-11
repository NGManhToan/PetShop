using Microsoft.AspNetCore.Mvc;

namespace PetShop.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
