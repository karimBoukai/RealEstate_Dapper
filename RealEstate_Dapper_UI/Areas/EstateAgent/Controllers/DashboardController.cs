using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    [Authorize] // uses the signed-in user id (GetUserID); anonymous requests used to throw NullReferenceException
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
