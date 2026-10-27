using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.ServiceDtos;
using RealEstate_Dapper_Api.Repositories.ServiceRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ServiceList()
        {
            var values = await _serviceRepository.GetAllServices();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDtos createServiceDtos)
        {
            await _serviceRepository.CreateService(createServiceDtos);
            return Ok("The Service has been successfully added.");
        }
         [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _serviceRepository.DeleteService(id);
            return Ok("The Service has been successfully deleted.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(UpdateServiceDtos updateServiceDtos)
        {
            await _serviceRepository.UpdateService(updateServiceDtos);
            return Ok("The Service has been successfully updated.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var values = await _serviceRepository.GetService(id);
            return Ok(values);
        }
    }
}
