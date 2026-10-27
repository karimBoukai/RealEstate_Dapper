using RealEstate_Dapper_Api.Dtos.ProductDtos;

namespace RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.LastProductsRepository
{
    public interface ILast5ProductsRepository
    {

        Task<List<ResultLast5ProductWithCategoryDtos>> GetLast5Products(int id);
    }
}
