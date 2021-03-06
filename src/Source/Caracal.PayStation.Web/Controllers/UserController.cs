using System.Threading;
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
        public async Task<ActionResult<LoginResponseViewModel>> LoginAsync([FromBody] LoginRequestViewModel request, CancellationToken cancellationToken) {
            var response = await _loginGateway.LoginAsync(_mapper.Map<LoginRequest>(request), cancellationToken);
            
            if(string.IsNullOrWhiteSpace(response?.Token))
                return Ok(new LoginResponseViewModel("LoginAsync failed", false));

            Cookies.SetUserToken(Response, response.Token);
            
            return Ok(new LoginResponseViewModel($"Welcome {request.Username}", true));
        }
        
        [HttpPost("logout")]
        public ActionResult<LoginResponseViewModel> LogoutAsync(CancellationToken cancellationToken) {
            Cookies.SetUserToken(Response, string.Empty);
            
            return Ok(new LoginResponseViewModel("Logged out", true));
        }
    }
}