using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.PropertyAmenitiesRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyAmenitiesController : ControllerBase
    {
        private readonly IPropertyAmenitiesRepository _propertyAmenitiesRepository;

        public PropertyAmenitiesController(IPropertyAmenitiesRepository propertyAmenitiesRepository)
        {
            _propertyAmenitiesRepository = propertyAmenitiesRepository;
        }

        [HttpGet("ResultPropertyAmenityByStatusTrue/{id}")]
        public async Task<IActionResult> ResultPropertyAmenityByStatusTrue(int id)
        {
            var values = await _propertyAmenitiesRepository.ResultPropertyAmenitiesByStatusTrue(id);
            return Ok(values);
        }

        [HttpGet("GetAllAmenitiesByProductID/{id}")]
        public async Task<IActionResult> GetAllAmenitiesByProductID(int id)
        {
            var values = await _propertyAmenitiesRepository.GetAllAmenitiesByProductID(id);
            return Ok(values);
        }
    }
}
