using Dapper;
using RealEstate_Dapper_Api.Dtos.ProductImageDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using RealEstate_Dapper_Api.Repositories.MessagesRepository;

namespace RealEstate_Dapper_Api.Repositories.ProductImageRepository
{
    public class ProductImageRepository : IProductImageRepository
    {
            private readonly Context _context;
        public ProductImageRepository(Context context)
        {
            _context = context;
        }


        public async Task <List<GetProductImageByProductIdDtos>> GetProductImageByProductId(int id)
        {
            string query = "Select * From ProductImage Where ProductID = @ProductID";
            var parameters = new DynamicParameters();
            parameters.Add("ProductID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductImageByProductIdDtos>(query,parameters);
                return values.ToList();
            }

            
        }
    }
}
