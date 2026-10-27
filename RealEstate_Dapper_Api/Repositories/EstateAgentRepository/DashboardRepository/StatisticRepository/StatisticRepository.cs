using Dapper;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.StatisticRepository
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly Context _context;

        public StatisticRepository(Context context)
        {
            _context = context;
        }
        public int AllProductCount()
        {
            string query = "Select Count(*) From Product";

            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query);
                return value;
            }
        }

        public int ProductCountByEmployeeID(int id)
        {
            string query = "Select Count(*) From Product Where AppUserID=@AppUserID";
            var parameters = new DynamicParameters();
            parameters.Add("@AppUserID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query, parameters);
                return value;
            }
        }

        public int ProductCountByStatusFalse(int id)
        {
            string query = "Select Count(*) From Product Where AppUserID=@AppUserID And ProductStatus=0";
            var parameters = new DynamicParameters();
            parameters.Add("@AppUserID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query, parameters);
                return value;
            }
        }

        public int ProductCountByStatusTrue(int id)
        {
            string query = "Select Count(*) From Product Where AppUserID=@AppUserID And ProductStatus=1";
            var parameters = new DynamicParameters();
            parameters.Add("@AppUserID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = connection.QueryFirstOrDefault<int>(query, parameters);
                return value;
            }
        }


    }
}
