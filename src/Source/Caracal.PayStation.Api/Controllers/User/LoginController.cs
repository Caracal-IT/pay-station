using System.Threading.Tasks;
using Caracal.PayStation.Api.Models.User;
using Caracal.PayStation.Application.Builders.Infrastructure;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers.User {
    [ApiController]
    [Route("users/[controller]")]
    public class LoginController: ControllerBase {
        private InfrastructureUseCaseBuilder _infrastructure;

        public LoginController(InfrastructureUseCaseBuilder infrastructure) {
            _infrastructure = infrastructure;
        }
        
        [HttpPost]
        public async Task<ObjectResult> Post([FromBody] Login login) {
            var uc = _infrastructure.Build<LoginUseCase>();
            var req = new LoginRequest(login.Username, login.Password, login.TenantId);
            var resp = await uc.Execute(req);
            
            return Ok(resp.UserId);
        }
    }
}