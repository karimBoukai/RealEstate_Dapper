using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.AppUserRepository;
using RealEstate_Dapper_Api.Repositories.BottomGridRepositories;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUserController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAppUserByProductID(int id)
        {
            var values = await _appUserRepository.GetAppUserByProductID( id);
            return Ok(values);
        }
    }
}
