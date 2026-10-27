using Dapper;
using RealEstate_Dapper_Api.Dtos.ProductDetailsDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultProductDtos>> GetAllProductAsync()
        {
            const string query = "SELECT * FROM Product";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDtos>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDtos>> GetAllProductWithCategoriesAsync()
        {
            string query = "SELECT ProductID, Title, Price, City, District, CategoryName, CoverImage, ProductStatus,SlugUrl Type, Address, DealOfTheDay FROM Product inner join Category on Product.ProductCategory=Category.CategoryID";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDtos>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDtos>> GetLast5ProductsAsync()
        {
            string query = "SELECT TOP(5) ProductID ,Title , Price ,City ,District ,CategoryName,ProductCategory,AdvertisementDate  FROM Product Inner Join Category On Product.ProductCategory= Category.CategoryID Where Type='Rent' ORDER BY ProductID DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDtos>(query);
                return values.ToList();
            }

        }

        public async Task<List<ResultProductAdvertsListWithCategoryByEmployeeDtos>> GetProductAdvertsListByEmployeeAsyncByTrue(int id)
        {
            string query = "SELECT ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, " +
                "DealOfTheDay FROM Product inner join Category on Product.ProductCategory=Category.CategoryID where AppUserID = @AppUserID And ProductStatus=1";

            var parameters = new DynamicParameters();
            parameters.Add("@AppUserID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertsListWithCategoryByEmployeeDtos>(query, parameters);
                return values.ToList();
            }

        }
        public async Task<List<ResultProductAdvertsListWithCategoryByEmployeeDtos>> GetProductAdvertsListByEmployeeAsyncByFalse(int id)
        {
            string query = "SELECT ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay FROM Product inner join Category on Product.ProductCategory=Category.CategoryID where AppUserID = @AppUserID And ProductStatus=0";

            var parameters = new DynamicParameters();
            parameters.Add("@AppUserID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertsListWithCategoryByEmployeeDtos>(query, parameters);
                return values.ToList();
            }

        }

        public async Task ProductDealOfTheDayeStatusChangeToFalse(int id)
        {
            string query = @"UPDATE Product SET DealOfTheDay=0 where ProductID = @ProductID";

            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", id);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task ProductDealOfTheDayeStatusChangeToTrue(int id)
        {
            string query = @"UPDATE Product SET DealOfTheDay=1  where ProductID = @ProductID";

            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", id);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task CreateProduct(CreateProductDtos product)
        {
            string query = @"INSERT INTO Product 
                    (Title, Price, City, District, CoverImage, Address, Description, 
                     Type, DealOfTheDay, AdvertisementDate, ProductStatus, 
                     ProductCategory, AppUserID) 
                     VALUES 
                    (@Title, @Price, @City, @District, @CoverImage, @Address, @Description, 
                     @Type, @DealOfTheDay, @AdvertisementDate, @ProductStatus, 
                     @ProductCategory, @AppUserID)";

            var parameters = new DynamicParameters();

            parameters.Add("@Title", product.Title);
            parameters.Add("@Price", product.Price);
            parameters.Add("@City", product.City);
            parameters.Add("@District", product.District);
            parameters.Add("@CoverImage", product.CoverImage);
            parameters.Add("@Address", product.Address);
            parameters.Add("@Description", product.Description);
            parameters.Add("@Type", product.Type);
            parameters.Add("@DealOfTheDay", product.DealOfTheDay);
            parameters.Add("@AdvertisementDate", product.AdvertisementDate);
            parameters.Add("@ProductStatus", product.ProductStatus);
            parameters.Add("@ProductCategory", product.ProductCategory);
            parameters.Add("@AppUserID", product.AppUserID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetProductByProductIDDtos> GetProductByProductID(int id)
        {
            string query = "SELECT ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay , AdvertisementDate ,Description ,SlugUrl FROM Product inner join Category on Product.ProductCategory=Category.CategoryID Where ProductID=@ProductID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryAsync<GetProductByProductIDDtos>(query, parameters);
                return value.FirstOrDefault();
            }
        }

        public async Task<GetProductDetailsByProductIDDtos> GetProductDetailsByProductID(int id)
        {
            string query = "SELECT * From ProductDetails Where ProductID =@ProductID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryAsync<GetProductDetailsByProductIDDtos>(query, parameters);
                return value.FirstOrDefault();
            }



        }

        public async Task<List<ResultProductWithSearchListDtos>> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryID, string city)
        {
            string query = @"
        SELECT
            p.ProductID,
            p.Title,
            p.Price,
            p.City,
            p.District,
            c.CategoryName,
            p.CoverImage,
            p.Type,
            p.Address,
            p.DealOfTheDay
        FROM Product p
        INNER JOIN Category c ON p.ProductCategory = c.CategoryID
        WHERE
            p.ProductStatus = 1
            AND (@searchKeyValue = '' OR p.Title LIKE '%' + @searchKeyValue + '%')
            AND (@propertyCategoryID = 0 OR p.ProductCategory = @propertyCategoryID)
            AND (@city = '' OR p.City = @city)
    ";

            var parameters = new DynamicParameters();
            parameters.Add("@searchKeyValue", searchKeyValue ?? "");
            parameters.Add("@propertyCategoryID", propertyCategoryID);
            parameters.Add("@city", city ?? "");

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithSearchListDtos>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDtos>> GetProductByDealOfTheDayTrueWithCategoryAsync()
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID Where DealOfTheDay=1";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDtos>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast3ProductWithCategoryDtos>> GetLast3ProductsAsync()
        {
            string query = "SELECT TOP(3) ProductID ,Title , Price ,City ,District ,CategoryName,ProductCategory,AdvertisementDate,CoverImage,Description  FROM Product Inner Join Category On Product.ProductCategory= Category.CategoryID Where Type='Rent' ORDER BY ProductID DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast3ProductWithCategoryDtos>(query);
                return values.ToList();
            }
        }

        public async Task<List<GetProductsForAdsPageDtos>> GetActiveAdsOnly()
        {
            string query = "SELECT Price, Address, City, District, Type, CoverImage,ProductStatus , ProductID , SlugUrl , CategoryName FROM Product inner join Category on Product.ProductCategory=Category.CategoryID where ProductStatus=1";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductsForAdsPageDtos>(query);
                return values.ToList();


            }
        }

        public async Task ProductAdsPageStatusChangeToTrue(int id)
        {
            string query = @"UPDATE Product SET ProductStatus=1 where ProductID = @ProductID";

            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", id);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task ProductAdsPageStatusChangeToFalse(int id)
        {
            string query = @"UPDATE Product SET ProductStatus=0 where ProductID = @ProductID";

            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", id);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}