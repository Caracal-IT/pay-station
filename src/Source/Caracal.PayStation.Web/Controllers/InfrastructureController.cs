using System.Collections.Generic;
using Caracal.PayStation.Web.Helpers;
using Caracal.PayStation.Web.ViewModel.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class InfrastructureController : ControllerBase {
        private bool IsLoggedIn => !string.IsNullOrWhiteSpace(Cookies.GetUserToken(Request));
        
        [HttpGet("menu")]
        public ActionResult<MenuViewModel> GetMenu() => Ok(IsLoggedIn ? GetLoggedInMenu() : GetDefaultMenu());
        
        private MenuViewModel GetDefaultMenu() =>
            new (new List<MenuItemViewModel> {
                new("Home", "home"),
                new("Login", "login")
            });

        private MenuViewModel GetLoggedInMenu() =>
            new (new List<MenuItemViewModel> {
                new("Home", "home"),
                new("Logout", "logout"),
                new("Withdrawals", "withdrawals")
            });
    }
}