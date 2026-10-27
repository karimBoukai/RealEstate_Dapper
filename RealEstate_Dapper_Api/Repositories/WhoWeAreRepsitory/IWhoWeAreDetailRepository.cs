using RealEstate_Dapper_Api.Dtos.WhoWeAreDetailDtos;
using RealEstate_Dapper_Api.Dtos.WhoWeAreDtos;

namespace RealEstate_Dapper_Api.Repositories.WhoWeAreRepsitory
{
    public interface IWhoWeAreDetailRepository
    {
            Task<List<ResultWhoWeAreDetailDtos>> GetAllWhoWeAreDetailDtos();
            Task CreateWhoWeAreDetail(CreateWhoWeAreDetailDtos createWhoWeAreDetailDtos);
            Task DeleteWhoWeAreDetail(int id);
            Task UpdateWhoWeAreDetail(UpdateWhoWeAreDetailDtos categoryDtos);

            Task<GetByIDWhoWeAreDetailDtos> GetWhoWeAreDetail(int id);
    }
}
