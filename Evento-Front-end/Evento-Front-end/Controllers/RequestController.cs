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

        [HttpPost]
        public async Task<IActionResult> CreateRequest(int selectedServiceId, string description)
        {
            var requestDto = new RequestDTO
            {
                ServiceID = selectedServiceId,
                Description = description
            };

            await _httpClient.PostAsJsonAsync(
                "https://localhost:7251/api/requests",
                requestDto);

            return RedirectToAction("Requests", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRequest(int requestId, string description)
        {
            await _httpClient.DeleteAsync(
                $"https://localhost:7251/api/requests/{requestId}");

            return RedirectToAction("Requests", "Home");
        }
    }
}
