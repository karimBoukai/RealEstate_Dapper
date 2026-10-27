using System.Security.Claims;

namespace RealEstate_Dapper_UI.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginServices(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserID => _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;



    }
}
