namespace RealEstate_Dapper_Api.Dtos.AdsDtos
{
    public class ResultAdsListDtos
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }     
        public bool ProductStatus { get; set; } 
        public string SlugUrl { get; set; }
    }
}
