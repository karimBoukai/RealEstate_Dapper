using RealEstate_Dapper_Api.Dtos.PopularLocationsDtos;

namespace RealEstate_Dapper_Api.Repositories.PopularLocationsRepository
{
    public interface IPopularLoacationsRepository
    {
        Task<List<ResultPopularLoactionsDtos>> GetAllPopularLocations();
        Task CreatePopularLocations(CreatePopularLocations createPopularLocations);
        Task DeletePopularLocations(int id);
        Task UpdatePopularLocations(UpdatePopularLocations updatePopularLocations);

        Task<GetByIDPopularLocations> GetPopularLoactions(int id);



    }
}
