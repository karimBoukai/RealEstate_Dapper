using Dapper;
using RealEstate_Dapper_Api.Dtos.WhoWeAreDetailDtos;
using RealEstate_Dapper_Api.Dtos.WhoWeAreDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.WhoWeAreRepsitory
{
    public class WhoWeAreDetailRepository : IWhoWeAreDetailRepository
    {
        private readonly Context _context;
        public WhoWeAreDetailRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateWhoWeAreDetail(CreateWhoWeAreDetailDtos createWhoWeAreDetailDtos)
        {
            string query = "INSERT INTO WhoWeAreDetail (Title, Subtitle , Description1 , Description2) VALUES (@Title, @Subtitle ,@Description1 , @Description2)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createWhoWeAreDetailDtos.Title);
            parameters.Add("@Subtitle", createWhoWeAreDetailDtos.Subtitle);
            parameters.Add("@Description1", createWhoWeAreDetailDtos.Description1);
            parameters.Add("@Description2", createWhoWeAreDetailDtos.Description2);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteWhoWeAreDetail(int id)
        {
            string query = "DELETE FROM WhoWeAreDetail WHERE WhoWeAreDetailID = @WhoWeAreDetailID";
            var parameters = new DynamicParameters();
            parameters.Add("@WhoWeAreDetailID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateWhoWeAreDetail(UpdateWhoWeAreDetailDtos updateWhoWeAreDetailDtos)
        {
            string query = "UPDATE WhoWeAreDetail SET Title = @Title, Subtitle = @Subtitle , Description1=@Description1 , Description2=@Description2 WHERE WhoWeAreDetailID = @WhoWeAreDetailID";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", updateWhoWeAreDetailDtos.Title);
            parameters.Add("@Subtitle", updateWhoWeAreDetailDtos.Subtitle);
            parameters.Add("@Description1", updateWhoWeAreDetailDtos.Description1);
            parameters.Add("@Description2", updateWhoWeAreDetailDtos.Description2);
            parameters.Add("@WhoWeAreDetailID", updateWhoWeAreDetailDtos.WhoWeAreDetailID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultWhoWeAreDetailDtos>> GetAllWhoWeAreDetailDtos()
        {
            string query = "SELECT * FROM WhoWeAreDetail";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultWhoWeAreDetailDtos>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDWhoWeAreDetailDtos> GetWhoWeAreDetail(int id)
        {
            string query = "SELECT * FROM WhoWeAreDetail WHERE WhoWeAreDetailID = @WhoWeAreDetailID";
            var parameters = new DynamicParameters();
            parameters.Add("@WhoWeAreDetailID", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDWhoWeAreDetailDtos>(query, parameters);
                return values;
            }
        }
    }
}
