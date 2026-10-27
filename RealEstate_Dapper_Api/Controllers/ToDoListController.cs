using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.ContactRepository;
using RealEstate_Dapper_Api.Repositories.ToDoListRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        public readonly IToDoListRepository _toDoListRepository;

        public ToDoListController(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        [HttpGet("GetAllToDoListsAsync")]
        public async Task<IActionResult> GetAllToDoListsAsync()
        {
            var values = await _toDoListRepository.GetAllToDoLists();
            return Ok(values);
        }


    }
}
