using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.EmployeeDtos;
using RealEstate_Dapper_UI.Models;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public ProfileController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            // IMPORTANT: this must be the logged-in email
            // If User.Identity.Name is not email in your project, change it to the correct claim.
            var email = User.Identity?.Name;

            // API endpoint should return employee by mail
            // Swagger must have something like: Employees/GetEmployeeByMail?mail=...
            var response = await client.GetAsync($"Employees/GetEmployeeByMail?mail={email}");

            if (!response.IsSuccessStatusCode)
                return View(new UpdateEmployeeDtos());

            var json = await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateEmployeeDtos>(json);

            return View(value ?? new UpdateEmployeeDtos());
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(UpdateEmployeeDtos model)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Swagger must have PUT Employees (or similar)
            var response = await client.PutAsync("Employees", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("MyProfile");

            return View(model);
        }
    }
}
