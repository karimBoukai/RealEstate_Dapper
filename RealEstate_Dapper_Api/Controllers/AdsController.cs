using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.AdsRepository;
using RealEstate_Dapper_Api.Repositories.AdsRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {

        private readonly IAdsRepository _adsRepository;

        public AdsController(IAdsRepository adsRepository)
        {
            _adsRepository = adsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAdsList()
        {
            var values = await _adsRepository.GetAdsList();
            return Ok(values);
        }

    }
}
