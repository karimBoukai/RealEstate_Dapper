using Dapper;
using RealEstate_Dapper_Api.Dtos.PropertyAmenitiesDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using RealEstate_Dapper_Api.Repositories.ProductRepository;

namespace RealEstate_Dapper_Api.Repositories.PropertyAmenitiesRepository
{
    public class PropertyAmenitiesRepository : IPropertyAmenitiesRepository
    {

        private readonly Context _context;

        public PropertyAmenitiesRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultAllAmenitiesByProductIDDtos>> GetAllAmenitiesByProductID(int id)
        {
            string query = @"
        SELECT 
            p.ProductID,
            p.Title AS PropertyTitle,
            a.AmenityID,
            a.Title AS AmenityTitle,
            pa.Status
        FROM dbo.PropertyAmenity pa
        INNER JOIN dbo.Product p
            ON pa.PropertyID = p.ProductID
        INNER JOIN dbo.Amenities a
            ON pa.AmenityID = a.AmenityID
        WHERE p.ProductID = @ProductID
        ORDER BY pa.Status DESC";

            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAllAmenitiesByProductIDDtos>(query, parameters);
                return values.ToList();
            }
        }


        public async Task<List<ResultPropertyAmenitiesByStatusTrueDtos>> ResultPropertyAmenitiesByStatusTrue(int id)
        {
            string query = "SELECT PropertyAmenity.PropertyAmenityID, Amenities.Title " +
                "FROM PropertyAmenity " +
                "INNER JOIN Amenities ON Amenities.AmenityID = PropertyAmenity.AmenityID " +
                "WHERE PropertyAmenity.PropertyID = @PropertyID AND PropertyAmenity.Status = 1";

            var parameters = new DynamicParameters();
            parameters.Add("@propertyId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultPropertyAmenitiesByStatusTrueDtos>(query, parameters);
                return values.ToList();
            }
        }
    }

}