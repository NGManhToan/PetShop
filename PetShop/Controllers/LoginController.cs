﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PetShop.Models.Login;
using System.Security.Claims;
using WebApiTutorialHE.Service.Interface;

namespace PetShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("UserId");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("login")]
		public IActionResult Login(string? returnUrl)
        {
            if (returnUrl != null)
            {
                ViewBag.ReturnUrl = returnUrl;
                HttpContext.Session.SetString("Name", returnUrl);
                return View();
            }
                return View();
            
           
        }


        [HttpPost("login")]
		public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            var loginResponse = await _loginService.Login(loginModel);


            if (loginResponse == null)
            {
                // Login failed
                ModelState.AddModelError("", "Invalid login information.");
                TempData["ErrorMessage"] = "Invalid login information.";
                return View(loginModel);
            }

            if (loginResponse.Roles != "2")
            {
                // User doesn't have the required role
                ModelState.AddModelError("", "Access denied. You do not have the required role.");
                TempData["ErrorMessage"] = "Access denied. You do not have the required role.";
                return View(loginModel);
            }

            // Login successful
            var claims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, loginModel.Email),


                };
            HttpContext.Session.SetString("UserId", loginResponse.Id.ToString());
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // set properties if needed
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            string nameValue = HttpContext.Session.GetString("Name");
            if (!string.IsNullOrEmpty(nameValue) && Url.IsLocalUrl(nameValue))
            {
                return Redirect(nameValue);
            }

            return RedirectToAction("Index", "Home");
        }

		[HttpGet("changepassword")]
		public IActionResult ChangePassword()
		{
			return View();
		}

        

    }
}
