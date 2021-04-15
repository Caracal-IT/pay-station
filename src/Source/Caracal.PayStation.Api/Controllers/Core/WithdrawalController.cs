using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Application.Builders.Core;
using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using Microsoft.AspNetCore.Mvc;

using Model = Caracal.PayStation.Api.Models.Core.Withdrawals;
using Model2 = Caracal.PayStation.Api.Models.Core.Workflow;

namespace Caracal.PayStation.Api.Controllers.Core {
    /// <summary>
    /// Manages the withdrawals to be processed.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WithdrawalController : Controller {
        private WithdrawalsUseCaseBuilder _builder;
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
        public async Task<ActionResult<PagedData<Model.Withdrawal>>> GetWithdrawals([FromBody] PagedDataFilter request, CancellationToken cancellationToken) {
            return await TryExecute(GetWithdrawalAsync, cancellationToken);
            
            async Task<ActionResult<PagedData<Model.Withdrawal>>> GetWithdrawalAsync(CancellationToken token) {
                var uc = _builder.Build<GetWithdrawalsUseCase>();
                var resp = await uc.ExecuteAsync(_mapper.Map<GetWithdrawalsRequest>(request), token);
                return Ok(_mapper.Map<PagedData<Model.Withdrawal>>(resp));
            }
        }
        
        /// <summary>
        /// Get the withdrawals for the user.
        /// </summary>
        /// <param name="request">The statuses to update</param>
        /// <returns>The filtered withdrawals</returns>
        [HttpPost("status/update")]
        public async Task<ActionResult<IEnumerable<Model2.WithdrawalStatusUpdateResult>>> UpdateWithdrawalStatusAsync([FromBody] IEnumerable<Model2.WithdrawalStatus> request, CancellationToken cancellationToken) {
            return await TryExecute(UpdateStatusAsync, cancellationToken);
            
            async Task<ActionResult<IEnumerable<Model2.WithdrawalStatusUpdateResult>>> UpdateStatusAsync(CancellationToken token) {
                var uc = _builder.Build<ChangeWithdrawalStatusUseCase>();
                var resp = await uc.ExecuteAsync(_mapper.Map<ChangeWithdrawalStatusRequest>(request), cancellationToken);
                return Ok(_mapper.Map<IEnumerable<Model2.WithdrawalStatusUpdateResult>>(resp));
            }
        } 
    }
}