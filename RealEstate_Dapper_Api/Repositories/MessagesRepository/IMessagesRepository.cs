using RealEstate_Dapper_Api.Dtos.MessagesDtos;

namespace RealEstate_Dapper_Api.Repositories.MessagesRepository
{
    public interface IMessagesRepository
    {

        Task <List<ResultInBoxMessagesDtos>> GetInBoxLast3MessageListByReceiver(int id);
    }
}
