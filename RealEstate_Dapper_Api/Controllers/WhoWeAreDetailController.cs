using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.WhoWeAreDetailDtos;
using RealEstate_Dapper_Api.Repositories.CategoryRepository;
using RealEstate_Dapper_Api.Repositories.WhoWeAreRepsitory;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhoWeAreDetailController : ControllerBase
    {

        private readonly IWhoWeAreDetailRepository _whoWeAreDetailRepository;

        public WhoWeAreDetailController(IWhoWeAreDetailRepository whoWeAreRepository)
        {
            _whoWeAreDetailRepository = whoWeAreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> whoWeAreDetailList()
        {
            var values = await _whoWeAreDetailRepository.GetAllWhoWeAreDetailDtos();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWhoWeAreDetail(CreateWhoWeAreDetailDtos createWhoWeAreDetail)
        {
            await _whoWeAreDetailRepository.CreateWhoWeAreDetail(createWhoWeAreDetail);
            return Ok("The About Us has been successfully added.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhoWeAreDetail(int id)
        {
            await _whoWeAreDetailRepository.DeleteWhoWeAreDetail(id);
            return Ok("The About us has been successfully deleted.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateWhoWeAreDetailDtos updateWhoWeAreDetail)
        {
            await _whoWeAreDetailRepository.UpdateWhoWeAreDetail(updateWhoWeAreDetail);
            return Ok("The About has been successfully updated.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWhoWeAreDetail(int id)
        {
            var values = await _whoWeAreDetailRepository.GetWhoWeAreDetail(id);
            return Ok(values);
        }
    }
}
