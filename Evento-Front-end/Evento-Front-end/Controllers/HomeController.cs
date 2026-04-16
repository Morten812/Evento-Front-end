using System.Diagnostics;
using Evento_Front_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Threading.Tasks;
using Evento_Front_end.ViewModels;
using Evento_Front_end.DTOs;
using Evento_Front_end.ViewModels.Company;


namespace Evento_Front_end.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory)
        {
            _logger = logger;

            _httpClient = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetStringAsync("https://localhost:7251/api/hello");
            ViewBag.Message = result;
            
            return View();
        }

        public async Task<IActionResult> Companies()
        {

            var companies = await _httpClient
                .GetFromJsonAsync<List<CompanyDTO>>("https://localhost:7251/api/companies")
                ?? new List<CompanyDTO>();

            var vm = new ShowAllCompaniesVM
            {
                Companies = companies
            };

            return View(vm);
        }

        [Authorize]
        public IActionResult Tools()
        {
            return View();
        }

        [Authorize]
        public IActionResult Tasks()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CompanyDetails(int companyId)
        {
            var services = await _httpClient
                .GetFromJsonAsync<List<ServiceDTO>>(
                    $"https://localhost:7251/api/companies/{companyId}/services"
                    ) ?? new List<ServiceDTO>();

            var vm = new ShowCompanyDetailsVM
            {
                Services = services
            };

            return View(vm);
        }
    }
}
