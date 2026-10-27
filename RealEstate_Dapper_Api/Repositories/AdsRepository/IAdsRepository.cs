using RealEstate_Dapper_Api.Dtos.AdsDtos;

namespace RealEstate_Dapper_Api.Repositories.AdsRepository
{
    public interface IAdsRepository
    {
        Task<List<ResultAdsListDtos>> GetAdsList();

    }
}
