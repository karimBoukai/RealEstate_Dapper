using RealEstate_Dapper_Api.Dtos.BottomGridDtos;
using RealEstate_Dapper_Api.Dtos.ServiceDtos;

namespace RealEstate_Dapper_Api.Repositories.BottomGridRepositories
{
    public interface IBottomGridRepository
    {
    
            Task<List<ResultBottomGridDtos>> GetAllBottomGrid();
           Task   CreateBottomGrid(CreateBottomGridDtos createBottomGridDtos);
            Task DeleteBottomGrid(int id);
            Task UpdateBottomGrid(UpdateBottomGridDtos updateBottomGridDtos);

            Task<GetBottomGridDtos> GetBottomGrid(int id);



    }
}
