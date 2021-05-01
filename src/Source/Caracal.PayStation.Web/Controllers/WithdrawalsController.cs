using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;
using Caracal.PayStation.Web.ViewModel.Withdrawals;
using Caracal.PayStation.Web.ViewModel.Withdrawals.WithdrawalSearch;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WithdrawalController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly WithdrawalGateway _withdrawalGateway;
        
        public WithdrawalController(IMapper mapper, WithdrawalGateway withdrawalGateway) {
            _mapper = mapper;
            _withdrawalGateway = withdrawalGateway;
        }
        
        [HttpPost("filter")]
        public async Task<ActionResult<WithdrawalSearchResponseViewModel>> FilterAsync([FromBody] WithdrawalSearchRequestViewModel request, CancellationToken cancellationToken) {
            var resp = await _withdrawalGateway.GetWithdrawalsAsync(_mapper.Map<PagedDataFilter>(request), cancellationToken);
            var result = _mapper.Map<WithdrawalSearchResponseViewModel>(resp);
            return Ok(result);
        }
        
        [HttpPost("client/action")]
        public async Task<ActionResult<List<WorkflowActionViewModel>>> ProcessClientActionAsync(
            [FromBody] WorkflowActionListViewModel request,
            CancellationToken cancellationToken) {

            var resp = await _withdrawalGateway.ProcessClientActionAsync(_mapper.Map<List<WorkflowAction>>(request.Items), cancellationToken);
            var result = _mapper.Map<List<WorkflowActionViewModel>>(resp);
            return Ok(result);
        }
    }
}