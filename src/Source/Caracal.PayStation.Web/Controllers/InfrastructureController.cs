using System.Collections.Generic;
using Caracal.PayStation.Web.Helpers;
using Caracal.PayStation.Web.ViewModel.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class InfrastructureController : ControllerBase {
        private bool IsLoggedIn => !string.IsNullOrWhiteSpace(Cookies.GetUserToken(Request));

        [HttpGet("settings")]
        [ResponseCache(Location = ResponseCacheLocation.Any, VaryByHeader = "X-Version", Duration = 100000000)]
        public ActionResult<Dictionary<string, string>> Settings() {
            return Ok(new Dictionary<string, string> {
                {"[VERSION]", "1.0.0.3"},
                {"[WF]", "workflow/process[SELF]?v=[VERSION]"},
                {"[AUTH_API]", "[SELF]"},
                {"[DATA]", "assets/workflow/data/[SELF].json"},
                {"[WITHDRAWAL_API]", "withdrawal/[SELF]"},
                {"[CORE]", "Infrastructure/[SELF]"}
            });
        }
        
        [HttpGet("menu")]
        public ActionResult<MenuViewModel> GetMenu() => Ok(IsLoggedIn ? GetLoggedInMenu() : GetDefaultMenu());
        
        private static MenuViewModel GetDefaultMenu() =>
            new (new List<MenuItemViewModel> {
                new("Home", "home"),
                new("Login", "login")
            });

        private static MenuViewModel GetLoggedInMenu() =>
            new (new List<MenuItemViewModel> {
                new("Home", "home"),
                new("Logout", "logout"),
                new("Withdrawals", "withdrawals")
            });
    }
}