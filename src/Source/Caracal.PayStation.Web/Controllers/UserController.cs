using System.Threading.Tasks;
using AutoMapper;
using Caracal.PayStation.Web.Gateways.Security;
using Caracal.PayStation.Web.Gateways.Security.Model.Login;
using Caracal.PayStation.Web.Helpers;
using Caracal.PayStation.Web.ViewModel.Security.Login;
using Microsoft.AspNetCore.Mvc;


namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase {
        private readonly LoginGateway _loginGateway;
        private readonly IMapper _mapper;
        
        public UserController(IMapper mapper, LoginGateway loginGateway) {
            _mapper = mapper;
            _loginGateway = loginGateway;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseViewModel>> Login([FromBody] LoginRequestViewModel request) {
            var response = await _loginGateway.Login(_mapper.Map<LoginRequest>(request));
            
            if(string.IsNullOrWhiteSpace(response?.Token))
                return Ok(new LoginResponseViewModel("Login failed", false));

            Cookies.SetUserToken(Response, response.Token);
            
            return Ok(new LoginResponseViewModel($"Welcome {request.Username}", true));
        }
    }
}