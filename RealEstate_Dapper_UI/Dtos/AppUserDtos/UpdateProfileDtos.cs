namespace RealEstate_Dapper_UI.Dtos.AppUserDtos
{
    public class UpdateProfileDtos
    {
        public int AppUserID { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Username { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string? About { get; set; }
    }
}
