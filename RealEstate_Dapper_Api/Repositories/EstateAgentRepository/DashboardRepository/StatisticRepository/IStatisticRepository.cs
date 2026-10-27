namespace RealEstate_Dapper_Api.Repositories.EstateAgentRepository.DashboardRepository.StatisticRepository
{
    public interface IStatisticRepository
    {
        int ProductCountByEmployeeID(int id);
        int AllProductCount();
        int ProductCountByStatusTrue(int id);
        int ProductCountByStatusFalse(int id);


    }
}
