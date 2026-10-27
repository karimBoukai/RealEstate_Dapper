using RealEstate_Dapper_Api.Dtos.AppUserDtos;

namespace RealEstate_Dapper_Api.Repositories.AppUserRepository
{
    public interface IAppUserRepository
    {

        Task<GetAppUserByProductID> GetAppUserByProductID(int id);
    }
}
