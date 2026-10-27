using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.LastProductsRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateAgentLastProductController : ControllerBase
    {
        private readonly ILast5ProductsRepository _last5ProductsRepository;

        public EstateAgentLastProductController(ILast5ProductsRepository last5ProductsRepository)
        {
            _last5ProductsRepository = last5ProductsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLast5ProductAsync(int id)
        {
            var values = await _last5ProductsRepository.GetLast5Products(id);
            return Ok(values);
        }
    }
}
