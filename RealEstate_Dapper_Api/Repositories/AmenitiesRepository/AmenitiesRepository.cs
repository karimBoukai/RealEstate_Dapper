using Dapper;
using RealEstate_Dapper_Api.Dtos.AmenitiesDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.AmenitiesRepository
{
    public class AmenitiesRepository : IAmenitiesRepository
    {
        private readonly Context _context;
        public AmenitiesRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateAmenity(CreateAmenityDtos createAmenityDtos)
        {
            string query = "INSERT INTO Amenities (Title) VALUES (@Title)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createAmenityDtos.Title);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAmenity(int id)
        {
            string query = "DELETE FROM Amenities WHERE AmenityID = @AmenityID";
            var parameters = new DynamicParameters();
            parameters.Add("@AmenityID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public Task<List<GetAmenitiesByProductID>> GetAllAmenitiesByProductID(int id)
        {
            throw new NotImplementedException();
        }



        public async Task<List<ResultAllAmenitiesDtos>> GetAllAmenity()
        {
            string query = "SELECT * FROM Amenities";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAllAmenitiesDtos>(query);
                return values.ToList();
            }
        }

        public async Task<List<GetAmenitiesByProductID>> GetAmenitiesByProductID(int id)
        {
            string query = "Select * From Amenities Where ProductID = @ProductID";
            var parameters = new DynamicParameters();
            parameters.Add("ProductID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetAmenitiesByProductID>(query, parameters);
                return values.ToList();
            }
        }

        public async Task UpdateAmenity(UpdateAmenityDtos updateAmenityDtos)
        {
            string query = "UPDATE Amenities SET Title = @Title WHERE AmenityID = @AmenityID";

            var parameters = new DynamicParameters();
            parameters.Add("@Title", updateAmenityDtos.Title);
            parameters.Add("@AmenityID", updateAmenityDtos.AmenityID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<UpdateAmenityDtos> GetAmenityById(int id)
        {
            string query = "SELECT AmenityID, Title FROM Amenities WHERE AmenityID = @AmenityID";

            var parameters = new DynamicParameters();
            parameters.Add("@AmenityID", id);

            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<UpdateAmenityDtos>(query, parameters);
                return value;
            }
        }


    }
}
