using RealEstate_Dapper_Api.Dtos.EmployeeDtos;

namespace RealEstate_Dapper_Api.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<List<ResultEmployeeDtos>> GetAllEmployee();
        Task CreateEmployee(CreateEmployeeDtos createEmployeeDtos);
        Task DeleteEmployee(int id);
        Task UpdateEmployee(UpdateEmployeeDtos updateEmployeeDtos);

        Task<GetByIDEmployeeDtos> GetEmployee(int id);
    }
}
