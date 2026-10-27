using Dapper;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.StatisticsRepository
{
    public class StatisticsRepository: IStatisticsRepository
    {
        private readonly Context _context;
        public StatisticsRepository(Context context)
        {
            _context = context;
        }

        public  int ActiveCategoryCount()
        {
            string query = "SELECT Count(*) FROM Category Where CategoryStatus =1";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }


        }

        public int ActiveEmployeeCount()
        {
            string query = "SELECT Count(*) FROM Employee Where Status =1";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public int ApartmentCount()
        {
            string query = "SELECT Count(*) FROM Product Where Title like '%Apartment%'";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public decimal AvgProductPriceByRent()
        {
            string query = "Select Avg(Price) From Product Where Type='Rent'";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }

        }

        public decimal AvgProductPriceBySale()
        {
            string query = "Select Avg(Price) From Product Where Type='Sale'";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public int AvgRoomCount()
        {
            string query = "Select Avg(RoomCount) From ProductDetails";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public int CategoryCount()
        {
            string query = "SELECT Count(*) FROM Category";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }

        }

        public string CategoryNameByMaxProductCount()
        {
            string query = "SELECT TOP(1) CategoryName, COUNT(*) FROM Product INNER JOIN Category ON Product.ProductCategory = Category.CategoryID GROUP BY CategoryName ORDER BY COUNT(*) DESC ";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }

        }

        public int DifferentCityCount()
        {

            string query = "Select Count(Distinct (City)) From Product";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

            public string EmployeeNameByMaxProductCount()
            {
                string query = @"
                    SELECT TOP 1 e.Name
                    FROM Product p
                    INNER JOIN AppUser a ON p.AppUserID = a.UserID
                    INNER JOIN Employee e ON a.UserID = e.EmployeeID
                    GROUP BY e.Name
                    ORDER BY COUNT(*) DESC";

                using (var connection = _context.CreateConnection())
                {
                    return connection.QueryFirstOrDefault<string>(query);
                }
            }

        public decimal LastProductPrice()
        {
            string query = "Select Top(1) Price from Product Order By ProductID Desc";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<decimal>(query);
                return values;

            }
        }

        public string MaxCityNameByMaXProductCount()
        {
            string query = "Select Top(1) City,Count(*) as 'Product_Count'from Product Group By City Order by [Product_Count] Desc";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }
        }

        public string NewestBuildingYear()
        {
            string query = "Select Top(1) BuildYear from ProductDetails Order By BuildYear Desc";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }
        }

        public string OldestBuildingYear()
        {
            string query = "Select Top(1) BuildYear from ProductDetails Order By BuildYear ASC";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }
        }

        public int PassiveCategoryCount()
        {
            string query = "Select Count(*) From Category Where CategoryStatus=0";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public int ProductCount()
        {
            string query = "Select Count(*) From Product ";

            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }
    }
}
