using RealEstate_Dapper_Api.Dtos.PropertyAmenitiesDtos;

namespace RealEstate_Dapper_Api.Repositories.PropertyAmenitiesRepository
{
    public interface IPropertyAmenitiesRepository
    {
        Task<List<ResultAllAmenitiesByProductIDDtos>> GetAllAmenitiesByProductID(int id);

        Task<List<ResultPropertyAmenitiesByStatusTrueDtos>> ResultPropertyAmenitiesByStatusTrue(int id);


    }
}
