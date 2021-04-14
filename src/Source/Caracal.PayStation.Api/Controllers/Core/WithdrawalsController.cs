using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Application.Builders.Core;
using Caracal.PayStation.Application.UseCases.Withdrawals.FlushWithdrawals;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using Microsoft.AspNetCore.Mvc;

using Model = Caracal.PayStation.Api.Models.Core.Withdrawals;

namespace Caracal.PayStation.Api.Controllers.Core {
    /// <summary>
    /// Manages the withdrawals to be processed.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WithdrawalsController : Controller {
        private WithdrawalsUseCaseBuilder _builder;
        private readonly IMapper _mapper;

        /// <summary>
        /// Handles withdrawal requests
        /// </summary>
        /// <param name="mapper">Maps the view model and use case models.</param>
        /// <param name="builder">The withdrawals use case builder</param>
        public WithdrawalsController(IMapper mapper, WithdrawalsUseCaseBuilder builder) {
            _mapper = mapper;
            _builder = builder;
        }

        /// <summary>
        /// Get the withdrawals for the user.
        /// </summary>
        /// <param name="request">The request to select withdrawals</param>
        /// <returns>The filtered withdrawals</returns>
        [HttpPost("filter")]
        public async Task<PagedData<Model.Withdrawal>> GetWithdrawals([FromBody] PagedDataFilter request) {
            var uc = _builder.Build<GetWithdrawalsUseCase>();
            var resp = await uc.ExecuteAsync(_mapper.Map<GetWithdrawalsRequest>(request));
            return _mapper.Map<PagedData<Model.Withdrawal>>(resp);
        }
        
        /// <summary>
        /// Get the withdrawals for the user.
        /// </summary>
        /// <param name="request">The request to select withdrawals</param>
        /// <returns>The filtered withdrawals</returns>
        [HttpPost("flush")]
        public async Task<PagedData<Model.Withdrawal>> FlushWithdrawals([FromBody] PagedDataFilter request) {
            var uc = _builder.Build<FlushWithdrawalsUseCase>();
            FlushWithdrawalsResponse resp = await uc.ExecuteAsync(_mapper.Map<FlushWithdrawalsRequest>(request));
            return _mapper.Map<PagedData<Model.Withdrawal>>(resp);
        } 
    }
}