using Dapper;
using RealEstate_Dapper_Api.Dtos.BottomGridDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.BottomGridRepositories
{
    public class BottomGridRepository : IBottomGridRepository
    {

        private readonly Context _context;

        public BottomGridRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateBottomGrid(CreateBottomGridDtos createBottomGridDtos)
        {
            string query = "INSERT INTO BottomGrid (Icon, Title ,Description ) VALUES (@Icon, @Title , @Description)";
            var parameters = new DynamicParameters();
            parameters.Add("@Icon", createBottomGridDtos.Icon);
            parameters.Add("@Title", createBottomGridDtos.Title);
            parameters.Add("@Description", createBottomGridDtos.Description);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteBottomGrid(int id)
        {
            string query = "DELETE FROM BottomGrid WHERE BottomGridID = @BottomGridID";
            var parameters = new DynamicParameters();
            parameters.Add("@BottomGridID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }

        }

        public async Task<List<ResultBottomGridDtos>> GetAllBottomGrid()
        {
             string query = "SELECT * FROM BottomGrid";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultBottomGridDtos>(query);
                return values.ToList();
            }
        }

        public async Task<GetBottomGridDtos?> GetBottomGrid(int id)
        {
            string query = "SELECT * FROM BottomGrid WHERE BottomGridID = @BottomGridID";
            var parameters = new DynamicParameters();
            parameters.Add("@BottomGridID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetBottomGridDtos>(query, parameters);
                return values;
            }

        }

        public async Task UpdateBottomGrid(UpdateBottomGridDtos updateBottomGridDtos)
        {
            string query = "UPDATE BottomGrid SET Icon = @Icon, Title = @Title, Description = @Description WHERE BottomGridID = @BottomGridID";
            var parameters = new DynamicParameters();
            parameters.Add("@Icon", updateBottomGridDtos.Icon);
            parameters.Add("@Title", updateBottomGridDtos.Title);
            parameters.Add("@Description", updateBottomGridDtos.Description);
            parameters.Add("@BottomGridID", updateBottomGridDtos.BottomGridID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }


    }
}

