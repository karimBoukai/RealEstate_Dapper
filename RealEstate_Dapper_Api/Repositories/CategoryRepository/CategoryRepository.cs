using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using System.Numerics;
using System.Threading.Tasks;
namespace RealEstate_Dapper_Api.Repositories.CategoryRepository

{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateCategory(CreateCategoryDtos categoryDto)
        {
            string query = "INSERT INTO Category (CategoryName, CategoryStatus) VALUES (@CategoryName, @CategoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryName", categoryDto.CategoryName);
            parameters.Add("@CategoryStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCategory(int id)
        {

            string query = "DELETE FROM Category WHERE CategoryID = @CategoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryID ", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }


        }
        public async Task UpdateCategory(UpdateCategoryDtos categoryDtos)
        {
            string query = "UPDATE Category SET CategoryName = @CategoryName, CategoryStatus = @CategoryStatus WHERE CategoryID = @CategoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryName", categoryDtos.CategoryName);
            parameters.Add("@CategoryStatus", categoryDtos.CategoryStatus);
            parameters.Add("CategoryID", categoryDtos.CategoryID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }


        public async Task<List<ResultCategoryDtos>> GetAllCategory()
        {
            string query = "SELECT * FROM Category";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDtos>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDCategoryDtos> GetCategory(int id)
        {
            string query = "SELECT * FROM Category WHERE CategoryID = @CategoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDCategoryDtos>(query, parameters);
                return values;
            }
        }
    }
}
