using Dapper;
using RealEstate_Dapper_Api.Dtos.ServiceDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ServiceRepository
{
    public class ServiceRepository : IServiceRepository
    {

        private readonly Context _context;
        public ServiceRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateService(CreateServiceDtos createServiceDtos)
        {
            string query = "INSERT INTO Services (ServiceName, ServiceStatus) VALUES (@ServiceName, @ServiceStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@ServiceName", createServiceDtos.ServiceName);
            parameters.Add("@ServiceStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteService(int id)
        {
            string query = "DELETE FROM Services WHERE ServiceID = @ServiceID";
            var parameters = new DynamicParameters();
            parameters.Add("@ServiceID ", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task<List<ResultServiceDtos>> GetAllServices()
        {
            string query = "SELECT * FROM Services";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultServiceDtos>(query);
                return values.ToList();
            }
        }

        public Task GetAllSubFeaturesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetByIDServiceDtos> GetService(int id)
        {
            string query = "SELECT * FROM Services WHERE ServiceID = @ServiceID";
            var parameters = new DynamicParameters();
            parameters.Add("@ServiceID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDServiceDtos>(query, parameters);
                return values;
            }
        }
        public async Task UpdateService(UpdateServiceDtos ServiceDtos)
        {
            string query = "UPDATE Service SET ServiceName = @ServiceName, ServiceStatus = @ServiceStatus WHERE ServiceID = @ServiceID";
            var parameters = new DynamicParameters();
            parameters.Add("@ServiceName", ServiceDtos.ServiceName);
            parameters.Add("@ServiceStatus", ServiceDtos.ServiceStatus);
            parameters.Add("@ServiceID", ServiceDtos.ServiceID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
