using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.WhoWeAreDtos;
using RealEstate_Dapper_UI.Models;

namespace RealEstate_Dapper_UI.ViewComponents.HomePage
{
    public class _DefaultWhoWeAreComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _DefaultWhoWeAreComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var client2 = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            client2.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("WhoWeAreDetail");
            var responseMessage2 = await client2.GetAsync("Services");



            if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();

                var value = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDtos>>(jsonData) ?? new List<ResultWhoWeAreDetailDtos>();
                var value2 = JsonConvert.DeserializeObject<List<ResultServicesDtos>>(jsonData2);


                ViewBag.Title = value.Select(x => x.Title).FirstOrDefault();
                ViewBag.SubTitle = value.Select(x => x.Subtitle).FirstOrDefault();
                ViewBag.Description1 = value.Select(x => x.Description1).FirstOrDefault();
                ViewBag.Description2 = value.Select(x => x.Description2).FirstOrDefault();

                return View(value2 ?? new List<ResultServicesDtos>());
            }
           return View(new List<ResultServicesDtos>()); // never a bare View(): it would inherit the parent page model
        }

    }
}
