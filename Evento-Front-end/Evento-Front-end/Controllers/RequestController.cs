using Evento_Front_end.DTOs;
using Evento_Front_end.ViewModels;
using Evento_Front_end.ViewModels.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evento_Front_end.Controllers
{
    public class RequestController : Controller
    {
        public static List<RequestDTO> Requests = new();

        private readonly HttpClient _httpClient;

        public RequestController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpPost]
        public async Task<IActionResult> CreateRequest(int selectedServiceId, string description, int companyId, int customerId)
        {
            var dto = new
            {
                ServiceID = selectedServiceId,
                CustomerID = customerId,
                Description = description
            };

            await _httpClient.PostAsJsonAsync($"Https://localhost:7251/api/requests", dto);

            return RedirectToAction("CompanyDetails", "Home", new {companyId});
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRequestStatus(int requestId, RequestStatus status, int serviceId)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7251/api/requests/{requestId}/status",
                new RequestDTO
                {
                    Status = status
                });

            Console.WriteLine(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            return RedirectToAction("Requests", "Home", new {serviceId});
        }
    }
}
