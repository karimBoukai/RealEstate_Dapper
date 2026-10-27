using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.PopularLocationsDtos;
using RealEstate_Dapper_Api.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Repositories.PopularLocationsRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopularLocationsController : ControllerBase
    {
        private readonly IPopularLoacationsRepository _popularLoacationsRepository;

        public PopularLocationsController(IPopularLoacationsRepository popularLoacationsRepository)
        {
            _popularLoacationsRepository = popularLoacationsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> BottomGridList()
        {
            var values = await _popularLoacationsRepository.GetAllPopularLocations();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePopularLoacations(CreatePopularLocations createPopularLoacationsDtos)
        {
            await _popularLoacationsRepository.CreatePopularLocations(createPopularLoacationsDtos);
            return Ok("The PopularLoacations has been successfully added.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePopularLoacations(int id)
        {
            await _popularLoacationsRepository.DeletePopularLocations(id);
            return Ok("The Popular Loacation has been successfully deleted.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePopularLoacations(UpdatePopularLocations updatePopularLoacationsDtos)
        {
            await _popularLoacationsRepository.UpdatePopularLocations(updatePopularLoacationsDtos);
            return Ok("The Popular Loacation has been successfully updated.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPopularLoacations(int id)
        {
            var values = await _popularLoacationsRepository.GetPopularLoactions(id);
            return Ok(values);
        }

    }
}
