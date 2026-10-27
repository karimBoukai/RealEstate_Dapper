using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.PropertyAmenityDtos;
using RealEstate_Dapper_UI.Models;

namespace RealEstate_Dapper_UI.ViewComponents
{
    public class _PropertyAmenitiesComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _PropertyAmenitiesComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            // Correct endpoint:
            // /api/PropertyAmenities/ResultPropertyAmenityByStatusTrue/{id}
            var response = await client.GetAsync($"PropertyAmenities/ResultPropertyAmenityByStatusTrue/{id}");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultPropertyAmenityByStatusTrueDtos>());

            var json = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultPropertyAmenityByStatusTrueDtos>>(json);

            return View(values ?? new List<ResultPropertyAmenityByStatusTrueDtos>());
        }
    }
}
