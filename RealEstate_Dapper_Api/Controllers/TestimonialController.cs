using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Repositories.TestimonialRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {

        private readonly ITestimonialRepository _testimonialRepository;

        public TestimonialController(ITestimonialRepository testimonialRepository)
        {
            _testimonialRepository = testimonialRepository;
        }
        [HttpGet]
        public async Task<IActionResult> BottomGridList()
        {
            var values = await _testimonialRepository.GetAllTestimonial();
            return Ok(values);
        }

    }
}
