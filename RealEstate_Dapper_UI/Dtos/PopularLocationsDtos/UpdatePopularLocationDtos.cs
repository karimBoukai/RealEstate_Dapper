namespace RealEstate_Dapper_UI.Dtos.PopularLocationsDtos
{
    public class UpdatePopularLocationDtos
    {
        public int LocationID { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }

        public int PropertyCount { get; set; }


    }
}
