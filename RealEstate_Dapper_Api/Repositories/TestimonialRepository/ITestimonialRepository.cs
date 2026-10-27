using RealEstate_Dapper_Api.Dtos.ServiceDtos;
using RealEstate_Dapper_Api.Dtos.TestimonialDtos;

namespace RealEstate_Dapper_Api.Repositories.TestimonialRepository
{
    public interface ITestimonialRepository
    {

        Task<List<ResultTestimonialDtos>> GetAllTestimonial();
        Task CreateTestimonial(CreateTestimonialDtos createServiceDtos);
        Task DeleteTestimonial(int id);
        Task UpdateTestimonial(UpdateServiceDtos updateSeviceDtos);


    }
}
