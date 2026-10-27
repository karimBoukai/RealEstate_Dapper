namespace RealEstate_Dapper_Api.Dtos.PropertyAmenitiesDtos
{
    public class ResultAllAmenitiesByProductIDDtos
    {

        public int ProductID { get; set; }

        public string PropertyTitle { get; set; }

        public int AmenityID { get; set; }

        public string AmenityTitle { get; set; }

        public bool Status { get; set; }

    }
}
