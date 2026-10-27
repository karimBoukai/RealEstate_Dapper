using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_Api.Repositories.EmployeeRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeRepository.GetAllEmployee();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDtos createEmployeeDtos)
        {
           await  _employeeRepository.CreateEmployee(createEmployeeDtos);
            return Ok("The Employee has been successfully added.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
            return Ok("The Employee has been successfully deleted.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDtos updateEmployeeDtos)
        {
            await _employeeRepository.UpdateEmployee(updateEmployeeDtos);
            return Ok("The Employee has been successfully updated.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var values = await _employeeRepository.GetEmployee(id);
            return Ok(values);
        }
    }
}
