using Dapper;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.LoginDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using RealEstate_Dapper_Api.Tools;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(CreateLoginDtos loginDto)
        {
            string query = @"
                SELECT UserId, UserName, UserRole
                FROM AppUser
                WHERE UserName = @UserName AND Password = @Password";

            var parameters = new DynamicParameters();
            parameters.Add("@UserName", loginDto.UserName);
            parameters.Add("@Password", loginDto.Password);

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters);

                if (user == null)
                    return Unauthorized("Invalid username or password");

                GetCheckAppUserViewModel model = new GetCheckAppUserViewModel
                {
                    Id = user.UserId,
                    UserName = user.UserName,
                    EmployeeRole = user.UserRole   // ✅ mapping from AppUser.UserRole
                };

                var token = JWTTokenGenerator.GenerateToken(model);
                return Ok(token);
            }
        }
    }
}
