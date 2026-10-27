using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.BottomGridDtos;
using RealEstate_Dapper_Api.Repositories.BottomGridRepositories;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottomGridsController : ControllerBase
    {
        private readonly IBottomGridRepository _bottomGridRepository;

        public BottomGridsController(IBottomGridRepository bottomGridRepository)
        {
            _bottomGridRepository = bottomGridRepository;
        }
        [HttpGet]
        public async Task<IActionResult> BottomGridList()
        {
            var values = await _bottomGridRepository.GetAllBottomGrid();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBottomGrid(CreateBottomGridDtos createBottomGridDtos)
        {
            _bottomGridRepository.CreateBottomGrid(createBottomGridDtos);
            return Ok("The BottomGrid has been successfully added.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBottomGrid(int id)
        {
            _bottomGridRepository.DeleteBottomGrid(id);
            return Ok("The BottomGrid has been successfully deleted.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBottomGrid(UpdateBottomGridDtos updateBottomGridDtos)
        {
            _bottomGridRepository.UpdateBottomGrid(updateBottomGridDtos);
            return Ok("The BottomGrid has been successfully updated.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBottomGrid(int id)
        {
            var values = await _bottomGridRepository.GetBottomGrid(id);
            return Ok(values);
        }
    }
}
