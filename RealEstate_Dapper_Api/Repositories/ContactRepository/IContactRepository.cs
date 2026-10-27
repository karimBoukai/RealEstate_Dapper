using RealEstate_Dapper_Api.Dtos.ContactDtos;

namespace RealEstate_Dapper_Api.Repositories.ContactRepository
{
    public interface IContactRepository
    {
        Task<List<ResultContactDtos>> GetAllContact();
        Task CreateContact(CreateContactDtos ctaegoryDto);
        Task DeleteContact(int id);
        Task UpdateContact(UpdateContactDtos ContactDtos);

        Task<GetByIDContactDtos> GetContact(int id);
        Task<List<ResulLast4ContactDtos>> GetLast4Contact();


    }
}
