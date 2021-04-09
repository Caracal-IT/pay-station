using System.Threading.Tasks;
using AutoMapper;
using Caracal.PayStation.Api.Models.User;
using Caracal.PayStation.Application.Builders.Infrastructure;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers.User {
    [ApiController]
    [Route("users/[controller]")]
    public class LoginController: ControllerBase {
        private readonly InfrastructureUseCaseBuilder _infrastructure;
        private readonly IMapper _mapper;
        
        public LoginController(IMapper mapper, InfrastructureUseCaseBuilder infrastructure) {
            _mapper = mapper;
            _infrastructure = infrastructure;
        }
        
        [HttpPost]
        public async Task<ObjectResult> Post([FromBody] Login login) {
            var uc = _infrastructure.Build<LoginUseCase>();
            var req = _mapper.Map<LoginRequest>(login);
            
            var resp = await uc.Execute(req);
            
            return Ok(resp.UserId);
        }
    }
}