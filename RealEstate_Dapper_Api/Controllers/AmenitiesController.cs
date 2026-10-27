using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.AmenitiesDtos;
using RealEstate_Dapper_Api.Repositories.AmenitiesRepository;
using RealEstate_Dapper_Api.Repositories.ProductImageRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenitiesRepository _amenitiesRepository;

        public AmenitiesController(IAmenitiesRepository amenitiesRepository)
        {
            _amenitiesRepository = amenitiesRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAmenity()
        {
            var values = await _amenitiesRepository.GetAllAmenity();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAmenity(CreateAmenityDtos createAmenityDtos)
        {
            await _amenitiesRepository.CreateAmenity(createAmenityDtos);
            return Ok("The Amenity has been successfully added.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            await _amenitiesRepository.DeleteAmenity(id);
            return Ok("The Amenity has been successfully deleted.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAmenity(UpdateAmenityDtos updateAmenityDtos)
        {
            await _amenitiesRepository.UpdateAmenity(updateAmenityDtos);
            return Ok("The Amenity has been successfully updated.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmenityById(int id)
        {
            var values = await _amenitiesRepository.GetAmenityById(id);
            return Ok(values);
        }

    }
}