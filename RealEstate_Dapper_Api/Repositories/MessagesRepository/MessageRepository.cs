using Dapper;
using RealEstate_Dapper_Api.Dtos.MessagesDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.MessagesRepository
{
    public class MessageRepository : IMessagesRepository
    {
        private readonly Context _context;
        public MessageRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultInBoxMessagesDtos>> GetInBoxLast3MessageListByReceiver(int id)
        {
            string query = "Select Top(3) MessageID, Name, Subject, Detail, SendDate,UserImageUrl,IsRead From Message Inner Join AppUser On Message.Sender=AppUser.UserID Where Receiver=@ReceiverID Order By SendDate Desc";
            var parameters = new DynamicParameters();
            parameters.Add("@ReceiverID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultInBoxMessagesDtos>(query, parameters);
                return values.ToList();
            }
        }
    }
}
