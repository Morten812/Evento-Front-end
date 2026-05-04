using System.Diagnostics;
using Evento_Front_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Threading.Tasks;
using Evento_Front_end.ViewModels;
using Evento_Front_end.DTOs;
using Evento_Front_end.ViewModels.Company;
using Evento_Front_end.Controllers;


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

        public async Task<IActionResult> Companies(string searchTerm, List<string> municipalities, List<string> services)
        {

            var url = "https://localhost:7251/api/companies";

            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(searchTerm))
                queryParams.Add($"searchTerm={searchTerm}");

            if (municipalities != null && municipalities.Any())
                queryParams.AddRange(municipalities.Select(c => $"municipalities={c}"));

            if (services != null && services.Any())
                queryParams.AddRange(services.Select(c => $"services={c}"));

            if (queryParams.Any())
                url += "?" + string.Join("&", queryParams);

            var companies = await _httpClient
                .GetFromJsonAsync<List<CompanyDTO>>(url)
                ?? new List<CompanyDTO>();

            var vm = new ShowAllCompaniesVM
            {
                Companies = companies,
                SelectedMunicipalities = municipalities ?? new List<string>(),
                SelectedServices = services ?? new List<string>()
            };

            return View(vm);
        }

        [Authorize]
        public IActionResult Tools(string searchTerm)
        {
            return View();
        }

        [Authorize]
        public IActionResult Tasks(string searchTerm)
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Booking(int companyId)
        {
            var services = await _httpClient
                .GetFromJsonAsync<List<ServiceDTO>>($"https://localhost:7251/api/companies/{companyId}/services");

            var vm = new BookingVM
            {
                Services = services ?? new List<ServiceDTO>()
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CompanyDetails(int companyId)
        {
            var company = await _httpClient
                .GetFromJsonAsync<CompanyDTO>(
                $"https://localhost:7251/api/companies/{companyId}");
            

            var services = await _httpClient
                .GetFromJsonAsync<List<ServiceDTO>>(
                    $"https://localhost:7251/api/companies/{companyId}/services"
                    ) ?? new List<ServiceDTO>();

            var vm = new ShowCompanyDetailsVM
            {
                Company = company,
                Services = services ?? new List<ServiceDTO>()
            };

            return View(vm);
        }

        public async Task<IActionResult> Requests(int serviceId)
        {
            var requests = await _httpClient
                .GetFromJsonAsync<List<RequestDTO>>(
                $"https://localhost:7251/api/requests/{serviceId}"
                ) ?? new List<RequestDTO>();

            var vm = new RequestsVM
            {
                Requests = requests
            };

            return View(vm);
        }
    }
}
