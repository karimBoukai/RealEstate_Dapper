using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.MessagesDtos;
using RealEstate_Dapper_UI.Models;
using RealEstate_Dapper_UI.Services;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.ViewComponents.EsateAgentNavrbarViewComponents
{
    public class _NavbarLast3MessagescomponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginServices _loginServices;
        private readonly ApiSettings _apiSettings;
        public _NavbarLast3MessagescomponentPartial(IHttpClientFactory httpClientFactory, ILoginServices loginServices , IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _loginServices = loginServices;
            _apiSettings = apiSettings.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = _loginServices.GetUserID;
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Messages?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultInBoxMessagesDtos>>(jsondata);
                return View(values ?? new List<ResultInBoxMessagesDtos>());
            }

            return View(new List<ResultInBoxMessagesDtos>());
        }
    }
}

