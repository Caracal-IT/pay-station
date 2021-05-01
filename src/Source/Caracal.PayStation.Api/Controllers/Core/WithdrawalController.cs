using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using Caracal.PayStation.Application.UseCases.Withdrawals.ProcessWFClientAction;
using Microsoft.AspNetCore.Mvc;

using Model = Caracal.PayStation.Api.Models.Core.Withdrawals;

namespace Caracal.PayStation.Api.Controllers.Core {
    /// <summary>
    /// Manages the withdrawals to be processed.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WithdrawalController : Controller {
        private readonly IMapper _mapper;

        /// <summary>
        /// Handles withdrawal requests
        /// </summary>
        /// <param name="mapper">Maps the view model and use case models.</param>
        /// <param name="builder">The withdrawals use case builder</param>
        public WithdrawalController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Get the withdrawals for the user.
        /// </summary>
        /// <param name="request">The request to select withdrawals</param>
        /// <returns>The filtered withdrawals</returns>
        [HttpPost("filter")]
        public async Task<ActionResult<PagedData<Withdrawal>>> GetWithdrawals([FromServices] GetWithdrawalsUseCase uc,
            [FromBody] PagedDataFilter request, CancellationToken cancellationToken) {
            return await TryExecute(GetWithdrawalAsync, cancellationToken);

            async Task<ActionResult<PagedData<Withdrawal>>> GetWithdrawalAsync(CancellationToken token) {
                var resp = await uc.ExecuteAsync(_mapper.Map<GetWithdrawalsRequest>(request), token);
                return Ok(_mapper.Map<PagedData<Withdrawal>>(resp));
            }
        }

        /// <summary>
        /// Execute the workflow action for the client
        /// </summary>
        /// <param name="request">The request to execute</param>
        /// <returns>Executes a workflow action</returns>
        [HttpPost("client/action")]
        public async Task<ActionResult<List<Model.WorkflowAction>>> ProcessClientActionAsync(
            [FromServices] ProcessWFClientActionUseCase uc,
            [FromBody] List<Model.WorkflowAction> request, 
            CancellationToken cancellationToken) {
            return await TryExecute(ProcessAsync, cancellationToken);

            async Task<ActionResult<List<Model.WorkflowAction>>> ProcessAsync(CancellationToken token) {
                var resp = await uc.ExecuteAsync(new ProcessWFClientActionRequest {
                    Items = request.Select(i => new WorkflowAction {
                        WithdrawalId = i.WithdrawalId, 
                        Payload = i.Payload
                    }).ToList()
                }, token);

                return Ok(resp.Items.Select(i => new Model.WorkflowAction {
                    WithdrawalId = i.WithdrawalId,
                    Payload = i.Payload,
                    Succeeded = i.Succeeded
                }).ToList());
            }
        }
    }
}