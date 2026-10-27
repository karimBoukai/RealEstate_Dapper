using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Repositories.MessagesRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesRepository _messagesRepository ;

        public MessagesController(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetInBoxLast3MessageListByReceiver(int id)
        {
            
            var values = await _messagesRepository.GetInBoxLast3MessageListByReceiver(id);
            return Ok(values);
        }


    }
}
