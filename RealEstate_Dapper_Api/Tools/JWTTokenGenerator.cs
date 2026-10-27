using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate_Dapper_Api.Tools
{
    public class JWTTokenGenerator
    {
        public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim("UserName", model.UserName),
                new Claim("EmployeeRole", model.EmployeeRole.ToString()) // THIS IS THE KEY
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTTokenDefualts.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JWTTokenDefualts.Expire);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JWTTokenDefualts.VaildIssuer,
                audience: JWTTokenDefualts.VaildAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return new TokenResponseViewModel(
                tokenHandler.WriteToken(token),
                expireDate
            );
        }
    }
}
