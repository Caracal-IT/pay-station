using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase {
        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request) => 
            Ok(new LoginResponse($"Welcome <em style='color:hotpink'>{request.Username}</em>"));
    }

    public record LoginRequest(string Username, string Password);

    public record LoginResponse(string WelcomeMessage);
}