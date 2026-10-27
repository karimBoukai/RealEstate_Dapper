using Dapper;
using RealEstate_Dapper_Api.Dtos.ToDoListDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ToDoListRepository
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly Context _context;
        public ToDoListRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateToDoList(CreateToDoListDtos createToDoListDtos)
        {
            string query = "INSERT INTO ToDoList (Description, ToDoListStatus) VALUES (@Description, @ToDoListStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@Description", createToDoListDtos.Description);
            parameters.Add("@ToDoListStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task DeleteToDoList(int id)
        {
            string query = "DELETE FROM ToDoList WHERE ToDoListID = @ToDoListID";
            var parameters = new DynamicParameters();
            parameters.Add("@ToDoListID ", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task<List<ResultToDoListDtos>> GetAllToDoLists()
        {
            string query = "SELECT * FROM ToDoList";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultToDoListDtos>(query);
                return values.ToList();
            }
        }

        public Task<GetByIDToDoListDtos> GetToDoList(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateToDoList(UpdateToDoListDtos updateSeviceDtos)
        {
            throw new NotImplementedException();
        }
    }
}
