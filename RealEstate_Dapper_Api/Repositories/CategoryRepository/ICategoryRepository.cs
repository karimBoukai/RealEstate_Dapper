using RealEstate_Dapper_Api.Dtos.CategoryDtos;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDtos>> GetAllCategory();
        Task CreateCategory(CreateCategoryDtos ctaegoryDto);
        Task DeleteCategory(int id);
        Task UpdateCategory(UpdateCategoryDtos categoryDtos);

        Task<GetByIDCategoryDtos> GetCategory(int id);
    }

}
