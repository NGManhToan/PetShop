using Microsoft.AspNetCore.Mvc;
using PetShop.Models.Register;
using PetShop.Service.Interface;

namespace PetShop.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterAccountService _registerAccountService;

        public RegisterController(IRegisterAccountService registerAccountService)
        {
            _registerAccountService = registerAccountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterAccountModel registerAccountModel)
        {
            var register = await _registerAccountService.RegisterAccount(registerAccountModel);
            if (register == null)
            {
                TempData["ErrorMessage"] = "Thông tin chưa đầy đủ";
                return View(register);
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
