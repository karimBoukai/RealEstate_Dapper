using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstate_Dapper_UI.Dtos.LoginDtos;
using RealEstate_Dapper_UI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace RealEstate_Dapper_UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public LoginController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDtos createLoginDtos)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var content = new StringContent(
                JsonSerializer.Serialize(createLoginDtos),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("Login/SignIn", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (tokenModel != null && tokenModel.Token != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();
                    // ✅ Ensure UserId exists as NameIdentifier (so GetUserID works)
                    var idClaim =
                        claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) ??
                        claims.FirstOrDefault(x => x.Type == "nameid") ??
                        claims.FirstOrDefault(x => x.Type == "sub") ??
                        claims.FirstOrDefault(x => x.Type == "UserId") ??
                        claims.FirstOrDefault(x => x.Type == "AppUserId") ??
                        claims.FirstOrDefault(x => x.Type == "AppUserID");

                    if (idClaim != null && claims.All(x => x.Type != ClaimTypes.NameIdentifier))
                    {
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, idClaim.Value));
                    }

                    // Ensure EmployeeRole claim exists
                    var roleClaim =
                        claims.FirstOrDefault(x => x.Type == "EmployeeRole") ??
                        claims.FirstOrDefault(x => x.Type == "RoleId") ??
                        claims.FirstOrDefault(x => x.Type == "roleId") ??
                        claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

                    if (roleClaim != null && claims.All(x => x.Type != "EmployeeRole"))
                    {
                        claims.Add(new Claim("EmployeeRole", roleClaim.Value));
                    }

                    claims.Add(new Claim("realestatetoken", tokenModel.Token));

                    var claimsIdentity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    var authProps = new AuthenticationProperties
                    {
                        ExpiresUtc = tokenModel.ExpireDate,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProps
                    );

                    // 🔵 ROLE BASED REDIRECT
                    var employeeRole = claims.FirstOrDefault(x => x.Type == "EmployeeRole")?.Value;

                    if (employeeRole == "3") // Manager
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else if (employeeRole == "1" || employeeRole == "5")
                    {
                        return RedirectToAction("Index", "Dashboard");

                    }

                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
