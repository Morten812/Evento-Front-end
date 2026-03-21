using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Evento_Front_end.ViewModels;
using Evento_Front_end.Models;

namespace Evento_Front_end.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var payload = new
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var response = await _httpClient.PostAsJsonAsync(
                "api/account/login",
                model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Login Failed");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT");
            return RedirectToAction("Index", "Home");
        }
    }
}
