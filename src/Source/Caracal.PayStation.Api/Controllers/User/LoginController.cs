using System.Threading.Tasks;
using AutoMapper;
using Caracal.PayStation.Api.Models.User;
using Caracal.PayStation.Application.Builders.Infrastructure;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers.User {
    /// <summary>
    /// The controller that handles the login request.
    /// </summary>
    [ApiController]
    [Route("users/[controller]")]
    public class LoginController: Controller {
        private readonly InfrastructureUseCaseBuilder _infrastructure;
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Create the controller.
        /// </summary>
        /// <param name="mapper">Maps the view model and use case models.</param>
        /// <param name="infrastructure">The infrastructure use case builder.</param>
        public LoginController(IMapper mapper, InfrastructureUseCaseBuilder infrastructure) {
            _mapper = mapper;
            _infrastructure = infrastructure;
        }
        
        /// <summary>
        /// Logs the user in
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /users/login
        ///     {
        ///        "tenantId": 207,
        ///        "username": "KateJ",
        ///        "password": "Nothing"
        ///     }
        ///
        /// </remarks>
        /// <param name="login">The login details of the user.</param>
        /// <returns>Context after the login succeeded.</returns>
        /// <response code="200">Context after the login succeeded.</response>
        /// <response code="401">The login failed, and the user is unauthorized.</response> 
        [HttpPost]
        [Produces("application/json")]
        public Task<ActionResult<UserContext>> Post([FromBody] Login login) {
            return TryExecute<UserContext>(async () => {
                var uc = _infrastructure.Build<LoginUseCase>();
                var req = _mapper.Map<LoginRequest>(login);
                var resp = await uc.ExecuteAsync(req);

                return Ok(_mapper.Map<UserContext>(resp));
            });
        }
    }
}