using System.Threading.Tasks;
using Caracal.PayStation.Web.Gateways.Security;
using Caracal.PayStation.Web.Model.Security.Login;
using Microsoft.AspNetCore.Mvc;


namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase {
        private readonly LoginGateway _loginGateway;
        
        public UserController(LoginGateway loginGateway) =>
            _loginGateway = loginGateway;
        
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request) => 
            Ok(await _loginGateway.Login(request));
    }
}