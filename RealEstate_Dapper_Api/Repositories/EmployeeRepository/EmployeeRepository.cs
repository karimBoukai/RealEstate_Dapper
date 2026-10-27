using Dapper;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultEmployeeDtos>> GetAllEmployee()
        {
            string query = "SELECT * FROM Employee";

            using (var connection = _context.CreateConnection()){
                var values = await connection.QueryAsync<ResultEmployeeDtos>(query);
                return values.ToList();

            }
            
        }

        public async Task CreateEmployee(CreateEmployeeDtos createEmployeeDtos)
        {
            string query = @"INSERT INTO Employee 
                             (Name, Title, Mail, PhoneNumber, ImageUrl, Status)
                             VALUES 
                             (@Name, @Title, @Mail, @PhoneNumber, @ImageUrl, @Status)";

            var parameters = new DynamicParameters();
            parameters.Add("@Name", createEmployeeDtos.Name);
            parameters.Add("@Title", createEmployeeDtos.Title);
            parameters.Add("@Mail", createEmployeeDtos.Mail);
            parameters.Add("@PhoneNumber", createEmployeeDtos.PhoneNumber);
            parameters.Add("@ImageUrl", createEmployeeDtos.ImageUrl);
            parameters.Add("@Status", true);
            parameters.Add("@Status", createEmployeeDtos.EmployeeRole);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteEmployee(int id)
        {
            string query = "DELETE FROM Employee WHERE AppUserID = @AppUserID";

            var parameters = new DynamicParameters();
            parameters.Add("@AppUserID", id);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<GetByIDEmployeeDtos> GetEmployee(int id)
        {
            string query = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID";

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeID", id);

            using var connection = _context.CreateConnection();
            var value = await connection.QueryFirstOrDefaultAsync<GetByIDEmployeeDtos>(query, parameters);
            return value;
        }

        public async Task UpdateEmployee(UpdateEmployeeDtos updateEmployeeDtos)
        {
            string query = @"UPDATE Employee 
                             SET Name = @Name,
                                 Title = @Title,
                                 Mail = @Mail,
                                 PhoneNumber = @PhoneNumber,
                                 ImageUrl = @ImageUrl,
                                 Status = @Status
                                 EmployeeRole = @EmployeeRole
                             WHERE EmployeeID = @EmployeeID";

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeID", updateEmployeeDtos.EmployeeID);
            parameters.Add("@Name", updateEmployeeDtos.Name);
            parameters.Add("@Title", updateEmployeeDtos.Title);
            parameters.Add("@Mail", updateEmployeeDtos.Mail);
            parameters.Add("@PhoneNumber", updateEmployeeDtos.PhoneNumber);
            parameters.Add("@ImageUrl", updateEmployeeDtos.ImageUrl);
            parameters.Add("@Status", updateEmployeeDtos.Status);
            parameters.Add("@Status", updateEmployeeDtos.EmployeeRole);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
