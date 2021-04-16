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
        [ResponseCache(Location = ResponseCacheLocation.Any, VaryByHeader = "X-Version", Duration = 604800)]
        public ActionResult<Dictionary<string, string>> Settings() {
            return Ok(new Dictionary<string, string> {
                {"[WF]", "workflow/process[SELF]"},
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