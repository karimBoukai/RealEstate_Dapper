namespace RealEstate_Dapper_Api.Dtos.PopularLocationsDtos
{
    public class GetByIDPopularLocations
    {
        public int LocationID { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }
        public int PropertyCount { get; set; }

    }
}
