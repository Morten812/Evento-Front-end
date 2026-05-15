using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Threading.Tasks;
using Evento_Front_end.Models;
using Evento_Front_end.DTOs;
using Evento_Front_end.Data;

namespace Evento_Front_end.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _httpClient
                .GetFromJsonAsync<List<CustomerDTO>>(
                    "https://localhost:7251/api/customers"
                    ) ?? new();

            return View(customers);
        }
    }
}

