using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Evento_Front_end.ViewModels;
using Evento_Front_end.Models;
using Evento_Front_end.DTOs;

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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7251/api/account/login",
                new LoginDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    RememberMe = model.RememberMe
                });

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("LOGIN RESPONSE:");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(responseBody);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("LOGIN FAILED DETAILS");
                Console.WriteLine(error);

                ModelState.AddModelError("", "Login Failed");
                return View(model);
            }

            var tokenResponse = await response.Content
                .ReadFromJsonAsync<LoginResponseDTO>();

            HttpContext.Session.SetString("JWT", tokenResponse.Token);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT");
            return RedirectToAction("Index", "Home");
        }
    }
}
