using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.PropertyImageDtos;

namespace RealEstate_Dapper_UI.ViewComponents.PropertySlider
{
    public class _PropertySliderComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PropertySliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client3.GetAsync("https://localhost:44387/api/ProductImage?id="+ id);
            if (responseMessage3.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage3.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<PropertyImageDtos>>(jsonData);
                return View(values ?? new List<PropertyImageDtos>());
            }
            return View(new List<PropertyImageDtos>()); // never a bare View(): it would inherit the parent page model

 
        }

    }
}
