
using RealEstate_Dapper_Api.Dtos.SubFeatureDtos;

namespace RealEstate_Dapper_Api.Repositories.SubFeatureRepositpry
{
    public interface ISubFeatureRepository
    {
        Task <List<ResultSubFeatureDtos>> GetAllSubFeatures();
        Task CreateSubFeatures(CreateSubFeatureDtos createSubFeatureDtos);
        Task DeleteSubFeature(int id);
        Task UpdateSubFeature(UpdateSubFeatureDtos updateSubFeatureDtos);

        Task<GetByIdSubFeatureSubFeatureDtos> GetSubFeatures(int id);
    }
}
