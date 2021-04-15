using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals;
using Caracal.PayStation.Web.ViewModel.Security.Withdrawals.WithdrawalSearch;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class Withdrawals : ControllerBase {
        private readonly IMapper _mapper;
        private readonly WithdrawalGateway _withdrawalGateway;
        
        public Withdrawals(IMapper mapper, WithdrawalGateway withdrawalGateway) {
            _mapper = mapper;
            _withdrawalGateway = withdrawalGateway;
        }
        
        [HttpPost("filter")]
        public async Task<ActionResult<WithdrawalSearchResponseViewModel>> Filter([FromBody] WithdrawalSearchRequestViewModel request) {
            var resp = await _withdrawalGateway.GetWithdrawalsAsync(_mapper.Map<PagedDataFilter>(request));
            return Ok(_mapper.Map<WithdrawalSearchResponseViewModel>(resp));
        }

        [HttpPost("flush")]
        public async Task<ActionResult<WithdrawalSearchResponseViewModel>> Flush() {
            var resp = await _withdrawalGateway.FlushWithdrawalsAsync(new PagedDataFilter());
            return _mapper.Map<WithdrawalSearchResponseViewModel>(new WithdrawalSearchResponseViewModel());
        }
    }
}