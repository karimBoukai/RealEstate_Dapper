using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.EmployeeDtos;
using RealEstate_Dapper_UI.Models;
using RealEstate_Dapper_UI.Services;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginServices _loginServices;
        private readonly ApiSettings _apiSettings;


        public EmployeeController(IHttpClientFactory httpClientFactory, ILoginServices loginServices, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _loginServices = loginServices;
            _apiSettings = apiSettings.Value;

        }

        public async Task<IActionResult> Index()
        {
            var user = User.Claims;
            var userId = _loginServices.GetUserID;

            var token = User.Claims.FirstOrDefault(x => x.Type == "realestatetoken")?.Value;
            if (token != null)
            {

            
            var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_apiSettings.BaseUrl); 
                var responseMessage = await client.GetAsync("Employee");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEmployeeDtos>>(jsondata);
                return View(values);
            }
             }
            return View();
        }
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDtos createEmployeeDtos)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var jsonData = JsonConvert.SerializeObject(createEmployeeDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reponseMessage = await client.PostAsync("Employee", stringContent);
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.DeleteAsync($"Employee/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Employee/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateEmployeeDtos>(jsondata);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDtos updateEmployeeDtos)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var jsonData = JsonConvert.SerializeObject(updateEmployeeDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reponseMessage = await client.PutAsync("Employee", stringContent);
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
