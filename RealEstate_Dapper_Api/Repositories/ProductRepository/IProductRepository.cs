
using RealEstate_Dapper_Api.Dtos.ProductDetailsDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;

namespace RealEstate_Dapper_Api.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<ResultProductDtos>> GetAllProductAsync();
        Task<List<ResultProductAdvertsListWithCategoryByEmployeeDtos>> GetProductAdvertsListByEmployeeAsyncByTrue(int id);
        Task<List<ResultProductAdvertsListWithCategoryByEmployeeDtos>> GetProductAdvertsListByEmployeeAsyncByFalse(int id);
        Task<List<ResultProductWithCategoryDtos>> GetAllProductWithCategoriesAsync();
        Task ProductDealOfTheDayeStatusChangeToTrue(int id);
        Task ProductDealOfTheDayeStatusChangeToFalse(int id);
        Task ProductAdsPageStatusChangeToTrue(int id);
        Task ProductAdsPageStatusChangeToFalse(int id);
        Task<List<ResultLast5ProductWithCategoryDtos>> GetLast5ProductsAsync();
        Task<List<ResultLast3ProductWithCategoryDtos>> GetLast3ProductsAsync();

        Task CreateProduct(CreateProductDtos createProductDtos);

        Task<GetProductByProductIDDtos> GetProductByProductID(int id);
        Task<GetProductDetailsByProductIDDtos> GetProductDetailsByProductID(int id);
        Task<List<ResultProductWithSearchListDtos>> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryID, string city);

        Task <List<ResultProductWithCategoryDtos>> GetProductByDealOfTheDayTrueWithCategoryAsync();
        Task <List<GetProductsForAdsPageDtos>> GetActiveAdsOnly();

    }
}
