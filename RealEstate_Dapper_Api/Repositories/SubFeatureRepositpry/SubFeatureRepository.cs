using Dapper;
using RealEstate_Dapper_Api.Dtos.SubFeatureDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.SubFeatureRepositpry
{
    public class SubFeatureRepository : ISubFeatureRepository
    {

        private readonly Context _context;
        public SubFeatureRepository(Context context)
        {
            _context = context;
        }
        public Task CreateSubFeatures(CreateSubFeatureDtos createSubFeatureDtos)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSubFeature(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultSubFeatureDtos>> GetAllSubFeatures()
        {
            string query = "SELECT * FROM SubFeature";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultSubFeatureDtos>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdSubFeatureSubFeatureDtos> GetSubFeatures(int id)
        {
            throw new NotImplementedException();



        }

        public Task UpdateSubFeature(UpdateSubFeatureDtos updateSubFeatureDtos)
        {
            throw new NotImplementedException();
        }
    }
}
