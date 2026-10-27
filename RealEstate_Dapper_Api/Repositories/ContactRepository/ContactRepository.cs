using Dapper;
using RealEstate_Dapper_Api.Dtos.ContactDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ContactRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly Context _context;
        public ContactRepository(Context context)
        {
            _context = context;
        }


        public async Task CreateContact(CreateContactDtos contactDtos)
        {
            string query = "INSERT INTO Contact (Name, Message ,Subject ,Email,SendDate ) VALUES (@ContactRepository, @Email,@Name,@Message ,@Subject)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", contactDtos.Name);
            parameters.Add("@Subject", contactDtos.Subject);
            parameters.Add("@Email", contactDtos.Email);
            parameters.Add("@Name", contactDtos.Message);
            parameters.Add("@SendDate", contactDtos.SendDate);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task DeleteContact(int id)
        {
            string query = "DELETE FROM Contact WHERE ContactID = @ContactID";
            var parameters = new DynamicParameters();
            parameters.Add("@ContactID ", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task<List<ResultContactDtos>> GetAllContact()
        {
            const string query = "SELECT * FROM Contact";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultContactDtos>(query);
                return values.ToList();
            }
        }

        public Task<GetByIDContactDtos> GetContact(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResulLast4ContactDtos>> GetLast4Contact()
        {
            string query = "SELECT TOP(4) *  FROM Contact ORDER BY ContactID DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResulLast4ContactDtos>(query);
                return values.ToList();
            }
        }

        public async Task UpdateContact(UpdateContactDtos ContactDtos)
        {
            string query = "UPDATE Contact SET Name = @Name, Email = @Email , Subject=@Subject , Message=@Message ,SendDate=@SendSDate WHERE ContactID = @ContactID";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", ContactDtos.Name);
            parameters.Add("@Subject", ContactDtos.Subject);
            parameters.Add("@SendDate", ContactDtos.SendDate);
            parameters.Add("@Message", ContactDtos.Message);
            parameters.Add("@Email", ContactDtos.Email);
            parameters.Add("ContactID", ContactDtos.ContactID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        
    }
}
