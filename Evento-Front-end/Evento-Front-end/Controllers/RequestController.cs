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
    public class RequestController : Controller
    {
        public static List<RequestDTO> Requests = new();

        private readonly HttpClient _httpClient;

        public RequestController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpPost]
        public async Task<IActionResult> CreateRequest(int selectedServiceId, string description, int companyId)
        {
            var dto = new
            {
                ServiceID = selectedServiceId,
                Description = description
            };

            await _httpClient.PostAsJsonAsync($"Https://localhost:7251/api/requests", dto);

            return RedirectToAction("CompanyDetails", "Home", new {companyId});
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRequestStatus(int requestId, RequestStatus status, int serviceId)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7251/api/requests/{requestId}/status",
                new RequestDTO
                {
                    Status = status
                });

            return RedirectToAction("Requests", "Home", new {serviceId});
        }
    }
}
