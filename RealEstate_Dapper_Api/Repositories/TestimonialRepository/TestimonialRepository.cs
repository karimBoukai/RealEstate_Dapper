using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.ServiceDtos;
using RealEstate_Dapper_Api.Dtos.TestimonialDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.TestimonialRepository
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly Context _context;
        public TestimonialRepository(Context context)
        {
            _context = context;
        }

        public Task CreateTestimonial(CreateTestimonialDtos createServiceDtos)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTestimonial(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultTestimonialDtos>> GetAllTestimonial()
        {
            string query = "SELECT * FROM Testimonial;";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultTestimonialDtos>(query);
                return values.ToList();
            }


        }

        public Task UpdateTestimonial(UpdateServiceDtos updateSeviceDtos)
        {
            throw new NotImplementedException();
        }
    }


}

