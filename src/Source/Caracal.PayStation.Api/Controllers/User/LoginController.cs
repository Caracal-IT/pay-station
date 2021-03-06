using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.PayStation.Api.Models.User;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers.User {
    /// <summary>
    /// The controller that handles the login request.
    /// </summary>
    [ApiController]
    [Route("users/[controller]")]
    public class LoginController: Controller {
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Create the controller.
        /// </summary>
        /// <param name="mapper">Maps the view model and use case models.</param>
        public LoginController(IMapper mapper) => _mapper = mapper;

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
        public async Task<ActionResult<UserContext>> PostAsync([FromServices] LoginUseCase uc,[FromBody] Login login, CancellationToken cancellationToken) {
            return await TryExecute(LoginAsync, cancellationToken);
            
            async Task<ActionResult<UserContext>> LoginAsync(CancellationToken token) {
                var req = _mapper.Map<LoginRequest>(login);
                var resp = await uc.ExecuteAsync(req, token);

                return Ok(_mapper.Map<UserContext>(resp));
            }
        }
    }
}