using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;
using Caracal.PayStation.Web.ViewModel;
using Caracal.PayStation.Web.ViewModel.Withdrawals.Status;
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
            return Ok(_mapper.Map<WithdrawalSearchResponseViewModel>(resp));
        }

        [HttpPost("status/change")]
        public async Task<ActionResult<ListViewModel<StatusUpdateResultViewModel>>> ChangeStatusAsync(ListViewModel<StatusViewModel> request, CancellationToken cancellationToken) {
            var response = await _withdrawalGateway.UpdateStatusAsync(_mapper.Map<IEnumerable<WithdrawalStatus>>(request.Items), cancellationToken);
            var resp = new ListViewModel<StatusUpdateResultViewModel> {
                Items = _mapper.Map<IEnumerable<StatusUpdateResultViewModel>>(response).ToList()
            };
            
            return Ok(resp);
        }
    }
}