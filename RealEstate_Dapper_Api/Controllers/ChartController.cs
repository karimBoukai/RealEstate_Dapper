using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.ChartRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IChartRepository _chartRepository;

        public ChartController(IChartRepository chartRepository)
        {
            _chartRepository = chartRepository;
        }
        [HttpGet]
        public async Task<IActionResult> BottomGridList()
        {
            var values = await _chartRepository.Get5CityForChart();
            return Ok(values);
        }




    }
}
