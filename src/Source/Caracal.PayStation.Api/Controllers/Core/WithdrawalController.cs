using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Application.Builders.Core;
using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers.Core {
    /// <summary>
    /// Manages the withdrawals to be processed.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WithdrawalController : Controller {
        private readonly WithdrawalsUseCaseBuilder _builder;
        private readonly IMapper _mapper;

        /// <summary>
        /// Handles withdrawal requests
        /// </summary>
        /// <param name="mapper">Maps the view model and use case models.</param>
        /// <param name="builder">The withdrawals use case builder</param>
        public WithdrawalController(IMapper mapper, WithdrawalsUseCaseBuilder builder) {
            _mapper = mapper;
            _builder = builder;
        }

        /// <summary>
        /// Get the withdrawals for the user.
        /// </summary>
        /// <param name="request">The request to select withdrawals</param>
        /// <returns>The filtered withdrawals</returns>
        [HttpPost("filter")]
        public async Task<ActionResult<PagedData<Withdrawal>>> GetWithdrawals([FromBody] PagedDataFilter request, CancellationToken cancellationToken) {
            return await TryExecute(GetWithdrawalAsync, cancellationToken);
            
            async Task<ActionResult<PagedData<Withdrawal>>> GetWithdrawalAsync(CancellationToken token) {
                var uc = _builder.Build<GetWithdrawalsUseCase>();
                var resp = await uc.ExecuteAsync(_mapper.Map<GetWithdrawalsRequest>(request), token);
                return Ok(_mapper.Map<PagedData<Withdrawal>>(resp));
            }
        }
        
        /// <summary>
        /// Get the withdrawals for the user.
        /// </summary>
        /// <param name="request">The statuses to update</param>
        /// <returns>The filtered withdrawals</returns>
        [HttpPost("status/update")]
        public async Task<ActionResult<IEnumerable<WithdrawalStatusUpdateResult>>> UpdateWithdrawalStatusAsync([FromBody] IEnumerable<WithdrawalStatus> request, CancellationToken cancellationToken) {
            return await TryExecute(UpdateStatusAsync, cancellationToken);
            
            async Task<ActionResult<IEnumerable<WithdrawalStatusUpdateResult>>> UpdateStatusAsync(CancellationToken token) {
                var uc = _builder.Build<ChangeWithdrawalStatusUseCase>();
                var resp = await uc.ExecuteAsync(_mapper.Map<ChangeWithdrawalStatusRequest>(request), cancellationToken);
                return Ok(_mapper.Map<IEnumerable<WithdrawalStatusUpdateResult>>(resp));
            }
        } 
    }
}