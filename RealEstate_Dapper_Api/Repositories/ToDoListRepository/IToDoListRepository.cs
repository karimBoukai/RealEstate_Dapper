using RealEstate_Dapper_Api.Dtos.ToDoListDtos;

namespace RealEstate_Dapper_Api.Repositories.ToDoListRepository
{
    public interface IToDoListRepository
    {
        Task<List<ResultToDoListDtos>> GetAllToDoLists();
        Task CreateToDoList(CreateToDoListDtos createToDoListDtos);
        Task DeleteToDoList(int id);
        Task UpdateToDoList(UpdateToDoListDtos updateSeviceDtos);

        Task<GetByIDToDoListDtos> GetToDoList(int id);
    }
}
