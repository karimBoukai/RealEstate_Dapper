using Dapper;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.LastProductsRepository
{
   

    public class Last5ProductsRepository : ILast5ProductsRepository
    {
        private readonly Context _context;

        public Last5ProductsRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultLast5ProductWithCategoryDtos>> GetLast5Products(int id)
        {
            string query = "SELECT TOP(5) ProductID ,Title , Price ,City ,District ,CategoryName,ProductCategory,AdvertisementDate " +
                " FROM Product Inner Join Category On Product.ProductCategory= " +
                "Category.CategoryID Where AppUserID =@AppUserID ORDER BY ProductID DESC";
            var parameters = new DynamicParameters();
            parameters.Add("@AppUserID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDtos>(query , parameters);
                return values.ToList();
            }
        }
    }
}
