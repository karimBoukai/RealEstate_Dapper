namespace RealEstate_Dapper_UI.Dtos.ProductDetailsDtos
{
    public class GetPRoductDetailsByIdDtos
    {

       
            public int productDetailID { get; set; }
            public int productSize { get; set; }
            public int bedRoomCount { get; set; }
            public int bathCount { get; set; }
            public int roomCount { get; set; }
            public int garageSize { get; set; }
            public int buildYear { get; set; }
            public decimal price { get; set; }
            public string location { get; set; }
            public string videoUrl { get; set; }
            public int productID { get; set; }
            public DateTime AdvertisementDate { get; set; }
            public int ProductSize { get; set; }
            public string ExteriorMaterial { get; set; }
            public string StructureType { get; set; }



    }
}
