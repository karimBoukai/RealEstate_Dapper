using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.ServiceDtos;

namespace RealEstate_Dapper_Api.Repositories.ServiceRepository
{
    public interface IServiceRepository
    {
        Task<List<ResultServiceDtos>> GetAllServices();
        Task CreateService(CreateServiceDtos createServiceDtos);
        Task DeleteService(int id);
        Task UpdateService(UpdateServiceDtos updateSeviceDtos);

        Task<GetByIDServiceDtos> GetService(int id);
        Task GetAllSubFeaturesAsync(int id);
    }
}
