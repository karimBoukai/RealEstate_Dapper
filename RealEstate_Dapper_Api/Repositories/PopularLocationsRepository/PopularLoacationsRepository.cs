using Dapper;
using RealEstate_Dapper_Api.Dtos.BottomGridDtos;
using RealEstate_Dapper_Api.Dtos.PopularLocationsDtos;
using RealEstate_Dapper_Api.Dtos.ServiceDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.PopularLocationsRepository
{
    public class PopularLoacationsrepository : IPopularLoacationsRepository
    {
        private readonly Context _context;

        public PopularLoacationsrepository(Context context)
        {
            _context = context;
        }


        public async Task CreatePopularLocations(CreatePopularLocations createPopularLocations)
        {
            string query = "INSERT INTO PopularLocation (CityName, ImageUrl) VALUES (@CityName, @ImageUrl)";
            var parameters = new DynamicParameters();
            parameters.Add("@CityName", createPopularLocations.CityName);
            parameters.Add("@ImageUrl", createPopularLocations.ImageUrl);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task DeletePopularLocations(int id)
        {
            string query = "DELETE FROM PopularLocation WHERE LocationID = @LocationID";
            var parameters = new DynamicParameters();
            parameters.Add("@LocationID ", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task<List<ResultPopularLoactionsDtos>> GetAllPopularLocations()
        {
            string query = "SELECT * FROM PopularLocation";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultPopularLoactionsDtos>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDPopularLocations> GetPopularLoactions(int id)
        {
            string query = "SELECT * FROM PopularLocation WHERE LocationID = @LocationID";
            var parameters = new DynamicParameters();
            parameters.Add("@LocationID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDPopularLocations>(query, parameters);
                return values;
            }
        }

        public async Task UpdatePopularLocations(UpdatePopularLocations updatePopularLocations)
        {

            string query = "UPDATE PopularLocation SET CityName = @CityName, ImageUrl = @ImageUrl WHERE LocationID = @LocationID";
            var parameters = new DynamicParameters();
            parameters.Add("@CityName", updatePopularLocations.CityName);
            parameters.Add("@ImageUrl", updatePopularLocations.ImageUrl);
            parameters.Add("@LocationID", updatePopularLocations.LocationID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
    
}
