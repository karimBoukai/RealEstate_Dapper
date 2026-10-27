using Dapper;
using RealEstate_Dapper_Api.Dtos.AdsDtos;
using RealEstate_Dapper_Api.Dtos.AmenitiesDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.AdsRepository
{
    public class AdsRepository : IAdsRepository
    {
        private readonly Context _context;
        public AdsRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultAdsListDtos>> GetAdsList()
        {

            string query = "Select * From Product Where ProductStatus = 1";
            var parameters = new DynamicParameters();
            parameters.Add("ProductID");
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAdsListDtos>(query, parameters);
                return values.ToList();
            }
        }
    }
}
