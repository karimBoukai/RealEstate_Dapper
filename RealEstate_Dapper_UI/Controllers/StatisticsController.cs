using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RealEstate_Dapper_UI.Controllers
{
    public class StatisticsController : Controller
    {
       private readonly IHttpClientFactory _httpClientFactory;
        public StatisticsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            #region Static 1
            var client = _httpClientFactory.CreateClient();
            var responseMessage = client.GetAsync("https://localhost:44387/api/Statistics/ActiveCategoryCount");
            var jsonData = await responseMessage.Result.Content.ReadAsStringAsync();
            ViewBag.ActiveCategoryCount = jsonData;
           
            #endregion static 1

            #region Static 2
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = client2.GetAsync("https://localhost:44387/api/Statistics/ActiveEmployeeCount");
            var jsonData2 = await responseMessage2.Result.Content.ReadAsStringAsync();
            ViewBag.ActiveEmployeeCount = jsonData2;
            
            #endregion static 2

            #region Static 3
            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = client3.GetAsync("https://localhost:44387/api/Statistics/ApartmentCount");
            var jsonData3 = await responseMessage3.Result.Content.ReadAsStringAsync();
            ViewBag.ApartmentCount = jsonData3;
            #endregion static 3

            #region Static 4
            var client4 = _httpClientFactory.CreateClient();
            var responseMessage4 = client4.GetAsync("https://localhost:44387/api/Statistics/AvgProductPriceByRent");
            var jsonData4 = await responseMessage4.Result.Content.ReadAsStringAsync();
            ViewBag.AvgProductPriceByRent = jsonData4;
            #endregion static4

            #region Static 5
            var client5 = _httpClientFactory.CreateClient();
            var responseMessage5 = client5.GetAsync("https://localhost:44387/api/Statistics/ApartmentCount");
            var jsonData5 = await responseMessage5.Result.Content.ReadAsStringAsync();
            ViewBag.ApartmentCount = jsonData5;
            #endregion static5

            #region Static 6
            var client6 = _httpClientFactory.CreateClient();
            var responseMessage6 = client6.GetAsync("https://localhost:44387/api/Statistics/AvgRoomCount");
            var jsonData6 = await responseMessage6.Result.Content.ReadAsStringAsync();
            ViewBag.AvgRoomCount = jsonData6;
            #endregion static6

            #region Static 7
            var client7 = _httpClientFactory.CreateClient();
            var responseMessage7 = client7.GetAsync("https://localhost:44387/api/Statistics/CategoryCount");
            var jsonData7 = await responseMessage7.Result.Content.ReadAsStringAsync();
            ViewBag.CategoryCount = jsonData7;
            #endregion static7


            #region Static 8
            var client8 = _httpClientFactory.CreateClient();
            var responseMessage8 = client8.GetAsync("https://localhost:44387/api/Statistics/CategoryNameByMaxProductCount");
            var jsonData8 = await responseMessage8.Result.Content.ReadAsStringAsync();
            ViewBag.CategoryNameByMaxProductCount = jsonData8;
            #endregion static8


            #region Static 9
            var client9 = _httpClientFactory.CreateClient();
            var responseMessage9 = client9.GetAsync("https://localhost:44387/api/Statistics/MaxCityNameByMaXProductCount");
            var jsonData9 = await responseMessage9.Result.Content.ReadAsStringAsync();
            ViewBag.MaxCityNameByMaXProductCount = jsonData9;
            #endregion static9


            #region Static 10
            var client10 = _httpClientFactory.CreateClient();
            var responseMessage10 = client10.GetAsync("https://localhost:44387/api/Statistics/DifferentCityCount");
            var jsonData10 = await responseMessage10.Result.Content.ReadAsStringAsync();
            ViewBag.DifferentCityCount = jsonData10;
            #endregion static10


            #region Static 11
            var client11 = _httpClientFactory.CreateClient();
            var responseMessage11 = client11.GetAsync("https://localhost:44387/api/Statistics/EmployeeNameByMaxProductCount");
            var jsonData11 = await responseMessage11.Result.Content.ReadAsStringAsync();
            ViewBag.EmployeeNameByMaxProductCount = jsonData11;
            #endregion static11


            #region Static 12
            var client12 = _httpClientFactory.CreateClient();
            var responseMessage12 = client12.GetAsync("https://localhost:44387/api/Statistics/LastProductPrice");
            var jsonData12 = await responseMessage12.Result.Content.ReadAsStringAsync();
            ViewBag.LastProductPrice = jsonData12;
            #endregion static12


            #region Static 13
            var client13 = _httpClientFactory.CreateClient();
            var responseMessage13 = client13.GetAsync("https://localhost:44387/api/Statistics/NewestBuildingYear");
            var jsonData13 = await responseMessage13.Result.Content.ReadAsStringAsync();
            ViewBag.NewestBuildingYear = jsonData13;
            #endregion static13


            #region Static 14
            var client14 = _httpClientFactory.CreateClient();
            var responseMessage14 = client14.GetAsync("https://localhost:44387/api/Statistics/OldestBuildingYear");
            var jsonData14 = await responseMessage14.Result.Content.ReadAsStringAsync();
            ViewBag.OldestBuildingYear = jsonData14;
            #endregion static14


            #region Static 15
            var client15 = _httpClientFactory.CreateClient();
            var responseMessage15 = client15.GetAsync("https://localhost:44387/api/Statistics/PassiveCategoryCount");
            var jsonData15 = await responseMessage15.Result.Content.ReadAsStringAsync();
            ViewBag.PassiveCategoryCount = jsonData15;
            #endregion static15


            #region Static 16
            var client16 = _httpClientFactory.CreateClient();
            var responseMessage16 = client16.GetAsync("https://localhost:44387/api/Statistics/ProductCount");
            var jsonData16 = await responseMessage16.Result.Content.ReadAsStringAsync();
            ViewBag.ProductCount = jsonData16;
            #endregion static16




            return View();
           





        }
    }
}
