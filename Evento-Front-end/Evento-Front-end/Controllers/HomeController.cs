using Evento_Front_end.Controllers;
using Evento_Front_end.DTOs;
using Evento_Front_end.Models;
using Evento_Front_end.ViewModels;
using Evento_Front_end.ViewModels.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;


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

            Console.WriteLine($"INDEX USER AUTH: {User.Identity?.IsAuthenticated}");
            
            return View();
        }

        public async Task<IActionResult> Companies(string searchTerm, List<string> municipalities, List<string> services)
        {

            var url = "https://localhost:7251/api/companies";

            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(searchTerm))
                queryParams.Add($"searchTerm={searchTerm}");

            if (municipalities != null && municipalities.Any())
                queryParams.AddRange(municipalities.Select(m => $"municipalities={m}"));

            if (services != null && services.Any())
                queryParams.AddRange(services.Select(s => $"services={s}"));

            if (queryParams.Any())
                url += "?" + string.Join("&", queryParams);

            var companies = await _httpClient
                .GetFromJsonAsync<List<CompanyDTO>>(url)
                ?? new List<CompanyDTO>();

            var allCompanies = await _httpClient
                .GetFromJsonAsync<List<CompanyDTO>>(
                "https://localhost:7251/api/companies")
                ?? new List<CompanyDTO>();

            var serviceTypes = await _httpClient
                .GetFromJsonAsync<List<string>>(
                "https://localhost:7251/api/companies/service-types")
                ?? new List<string>();

            var vm = new ShowAllCompaniesVM
            {
                Companies = companies,

                Locations = allCompanies
                .Where(c => !string.IsNullOrWhiteSpace(c.Region)
                         && !string.IsNullOrWhiteSpace(c.Municipality))
                .Select(c => new MunicipalityVM
                {
                    Region = c.Region,
                    Municipality = c.Municipality
                })
                .DistinctBy(x => new {x.Region, x.Municipality})
                .OrderBy(x => x.Region)
                .ThenBy(x => x.Municipality)
                .ToList(),

                Types = serviceTypes,
                SelectedMunicipalities = municipalities ?? new List<string>(),
                SelectedServices = services ?? new List<string>()
            };

            var role = GetCurrentRole();

            /*
            if (role != "Customer" && role != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            */

            return View(vm);
        }

        public string? GetCurrentRole()
        {
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrWhiteSpace(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            return jwt.Claims
                .FirstOrDefault(c => c.Type == "role")
                ?.Value;
        }

        public string? GetCurrentUser()
        {
            /*
            var authHeader = Request.Headers["Authorization"]
                .FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader))
                return null;

            var token = authHeader.Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            return jwt.Claims
                .FirstOrDefault(c =>
                c.Type == JwtRegisteredClaimNames.Sub)
                ?.Value;
            */

            return User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        }

        public IActionResult Tools(string searchTerm)
        {
            /*
            if (GetCurrentRole() != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            */

            return View();
        }

        public IActionResult Tasks(string searchTerm)
        {
            /*
            if (GetCurrentRole() != "Admin")
                return RedirectToAction ("AccessDenied", "Account");
            */

            return View();
        }

        public IActionResult Privacy()
        {
            /*
            if (GetCurrentRole() != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            */

            return View();
        }

        public async Task<IActionResult> Booking(int companyId, int customerId)
        {
            var services = await _httpClient
                .GetFromJsonAsync<List<ServiceDTO>>($"https://localhost:7251/api/companies/{companyId}/services");

            var customers = await _httpClient
                .GetFromJsonAsync<List<CustomerDTO>>("https://localhost:7251/api/customers") ?? new();

            var vm = new BookingVM
            {
                CustomerID = customerId,
                CompanyID = companyId,
                Services = services ?? new List<ServiceDTO>(),
                Customers = customers ?? new List<CustomerDTO>()
            };

            var role = GetCurrentRole();

            /*
            if (role != "Customer" && role != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            */

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


        public async Task<IActionResult> Requests(int companyId)
        {
            /*
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            }
            */

            /*
            var requests = await _httpClient
                .GetFromJsonAsync<List<RequestDTO>>(
                $"https://localhost:7251/api/requests/my-company/pending"
                ) ?? new();
            */
            
            /*
            var response = await _httpClient.GetAsync(
                "https://localhost:7251/api/requests/my-company/pending");
            

            Console.WriteLine(response.StatusCode);

            if (!response.IsSuccessStatusCode)
            {
                return Content($"Status: {response.StatusCode}");
            }
            */

            /*
            var token = HttpContext.Session.GetString("JWT");

            Console.WriteLine($"TOKEN: {token}");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            }
            */

            /*
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:7251/api/requests/company/me/pending");
            */

            /*
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            */

            /*
            Console.WriteLine($"URL: {request.RequestUri}");
            */

            /*
            var requests = await response.Content
                .ReadFromJsonAsync<List<RequestDTO>>()
                ?? new();
            */

            /*
            var response = await _httpClient.SendAsync(request);
            


            Console.WriteLine($"STATUS: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                Console.WriteLine("RESPONSE BODY:");
                Console.WriteLine(body);

                return Content($"Status: {response.StatusCode}\n\n{body}");
            }
            */

            var requests = await _httpClient
                .GetFromJsonAsync<List<RequestDTO>>(
                $"https://localhost:7251/api/requests/company/{companyId}/pending")
                ?? new();
            
            var companies = await _httpClient
                .GetFromJsonAsync<List<CompanyDTO>>(
                "https://localhost:7251/api/companies"
                ) ?? new();
            

            var vm = new RequestsVM
            {

                Companies = companies,

                RequestRows = requests.Select(r => new RequestRowVM
                {
                    CompanyID = r.CompanyID,
                    RequestID = r.RequestID,
                    Description = r.Description,
                    Status = r.Status.ToString(),
                    ServiceName = r.ServiceName,
                    CustomerName = r.CustomerName,

                    CreatedAt = r.CreatedAt.ToLocalTime(),
                }).ToList()
            };

            Console.WriteLine($"CompanyId in Requests(): {companyId}");

            /*
            var role = GetCurrentRole();

            if (role != "Manager" && role != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            */
            
            return View(vm);
        }

        public async Task<IActionResult> Jobs()
        {
            var jobs = await _httpClient
                .GetFromJsonAsync<List<RequestDTO>>(
                "https://localhost:7251/api/requests/jobs"
                ) ?? new();

            var vm = new RequestsVM
            {
                RequestRows = jobs.Select(r => new RequestRowVM
                {
                    RequestID = r.RequestID,
                    Description = r.Description,
                    Status = r.Status.ToString(),
                    ServiceName = r.ServiceName,
                    CustomerName = r.CustomerName,

                    RespondedAt = r.RespondedAt?.ToLocalTime()
                }).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> RequestHistory(int companyId)
        {
            var requests = await _httpClient
               .GetFromJsonAsync<List<RequestDTO>>(
               $"https://localhost:7251/api/requests/company/{companyId}/history"
               ) ?? new();

            var companies = await _httpClient
                .GetFromJsonAsync<List<CompanyDTO>>(
                "https://localhost:7251/api/companies"
                ) ?? new();

            var vm = new RequestsVM
            {
                Companies = companies,

                RequestRows = requests.Select(r => new RequestRowVM
                {
                    RequestID = r.RequestID,
                    Description = r.Description,
                    Status = r.Status.ToString(),
                    ServiceName = r.ServiceName,
                    CustomerName = r.CustomerName,

                    CreatedAt = r.CreatedAt.ToLocalTime(),
                    RequestedEnd = r.RequestedEnd?.ToLocalTime(),
                }).ToList()
            };

            return View(vm);
        }

        public IActionResult Members()
        {
            return View();
        }
    }
}
