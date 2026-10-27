using RealEstate_Dapper_Api.Dtos.AmenitiesDtos;

namespace RealEstate_Dapper_Api.Repositories.AmenitiesRepository
{
    public interface IAmenitiesRepository
    {


        Task CreateAmenity(CreateAmenityDtos createAmenityDtos);
        Task DeleteAmenity(int id);
        Task UpdateAmenity(UpdateAmenityDtos updateAmenityDtos);
        Task<List<ResultAllAmenitiesDtos>> GetAllAmenity();
        Task<List<GetAmenitiesByProductID>> GetAllAmenitiesByProductID(int id);
        Task<UpdateAmenityDtos> GetAmenityById(int id);


    }
}
