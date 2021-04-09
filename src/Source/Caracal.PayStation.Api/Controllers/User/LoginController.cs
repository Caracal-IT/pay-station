using Caracal.PayStation.Api.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers.User {
    [ApiController]
    [Route("users/[controller]")]
    public class LoginController: ControllerBase {
        [HttpPost]
        public string Post([FromBody] Login login) {
            return "OK " + login.Username;
        }
    }
}