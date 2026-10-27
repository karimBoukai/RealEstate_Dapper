using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.StatisticsRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }


        [HttpGet("ActiveCategoryCount")]
        public IActionResult ActiveCategoryCount()
        {

            return Ok(_statisticsRepository.ActiveCategoryCount());
        }
        //Active Employee Count
        [HttpGet("ActiveEmployeeCount")]
        public IActionResult ActiveEmployeeCount()
        {
            return Ok(_statisticsRepository.ActiveEmployeeCount());
        }

        //Apartment Count
        [HttpGet("ApartmentCount")]
        public IActionResult ApartmentCount()
        {
            return Ok(_statisticsRepository.ApartmentCount());
        }
        //Avrage Rent 
        [HttpGet("AvgProductPriceByRent")]
        public IActionResult AvgProductPriceByRent()
        {
            return Ok(_statisticsRepository.AvgProductPriceByRent());
        }//Avrage Sale 
        [HttpGet("AvgProductPriceBySale")]
        public IActionResult AvgProductPriceBySale()
        {
            return Ok(_statisticsRepository.AvgProductPriceBySale());
        }

        //Avrage Room Count 
        [HttpGet("AvgRoomCount")]
        public IActionResult AvgRoomCount()
        {
            return Ok(_statisticsRepository.AvgRoomCount());
        }
        //Category Count 
        [HttpGet("CategoryCount")]
        public IActionResult CategoryCount()
        {
            return Ok(_statisticsRepository.CategoryCount());
        }
        //Category Name By Max Product Count 
        [HttpGet("CategoryNameByMaxProductCount")]
        public IActionResult CategoryNameByMaxProductCount()
        {
            return Ok(_statisticsRepository.CategoryNameByMaxProductCount());
        }

        //City Name By Max Product Count
        [HttpGet("MaxCityNameByMaXProductCount")]
        public IActionResult MaxCityNameByMaXProductCount()
        {
            return Ok(_statisticsRepository.MaxCityNameByMaXProductCount());
        }

        //Different City Count
        [HttpGet("DifferentCityCount")]
        public IActionResult DifferentCityCount()
        {
            return Ok(_statisticsRepository.DifferentCityCount());
        }
        //Employee Name By Max Product Count
        [HttpGet("EmployeeNameByMaxProductCount")]
        public IActionResult EmployeeNameByMaxProductCount()
        {
            return Ok(_statisticsRepository.EmployeeNameByMaxProductCount());
        }
        //Last Product Price
        [HttpGet("LastProductPrice")]
        public IActionResult LastProductPrice()
        {
            return Ok(_statisticsRepository.LastProductPrice());
        }
        // Newwest Build Year
        [HttpGet("NewestBuildingYear")]
        public IActionResult NewestBuildingYear()
        {
            return Ok(_statisticsRepository.NewestBuildingYear());
        }
        //Oldest Build Year
        [HttpGet("OldestBuildingYear")]
        public IActionResult OldestBuildingYear()
        {
            return Ok(_statisticsRepository.OldestBuildingYear());
        }
        //Passive Category Count
        [HttpGet("PassiveCategoryCount")]
        public IActionResult PassiveCategoryCount()
        {
            return Ok(_statisticsRepository.PassiveCategoryCount());
        }
        //Product Count 
        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            return Ok(_statisticsRepository.ProductCount());

        }
    }
}
