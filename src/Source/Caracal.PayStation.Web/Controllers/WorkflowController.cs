using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;
using Caracal.PayStation.Web.ViewModel.Withdrawals.WithdrawalSearch;
using Caracal.PayStation.Web.ViewModel.Withdrawals.Workflow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController: ControllerBase {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly WithdrawalGateway _withdrawalGateway;
        
        public WorkflowController(IWebHostEnvironment environment, IMapper mapper, WithdrawalGateway withdrawalGateway) {
            _environment = environment;
            _mapper = mapper;
            _withdrawalGateway = withdrawalGateway;
        }

        [HttpGet("process/{name}")]
        [ResponseCache(Location = ResponseCacheLocation.Any, VaryByHeader = "X-Version", Duration = 120)]
        public ActionResult GetProcess(string name) {
            var workflowDir =  _environment.ContentRootFileProvider.GetDirectoryContents("Workflow");
            var process = workflowDir.FirstOrDefault(f => f.Name.Equals($"{name.ToLower()}.wf.json"));

            if (process == null)
                return NoContent();
            
            return new FileStreamResult(process.CreateReadStream(), "application/json");
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