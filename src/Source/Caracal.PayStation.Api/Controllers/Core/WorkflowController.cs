using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Activities.Workflows.Extensions;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers.Core {
    /// <summary>
    /// Manages the workflow actions 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController: Controller {
        [HttpPost("start/{name}")]
        public async Task<ActionResult<string>> Start(
            [FromServices] IWorkflowInvoker wfInvoker, 
            [FromServices] IWorkflowRegistry wfRegistry,
            [FromServices] IWorkflowDefinitionStore wfStore,
            string name,
            CancellationToken cancellationToken) {

            var correlationId = Guid.NewGuid().ToString();
            
            var definitions = await wfStore.ListAsync(VersionOptions.Published, cancellationToken);
            var wf = definitions.FirstOrDefault(d => d.Name.ToLower(CultureInfo.InvariantCulture) == name);
            if (wf == null) return NotFound();
                
            var input = new Variables(new [] {
                new KeyValuePair<string, object>("withdrawalId", "33")
            });

            var context = await wfInvoker.StartAsync(wf, input: input, correlationId: correlationId, cancellationToken: cancellationToken);
            
            return Ok($"Batched/{correlationId}");
        }

        [HttpPost("resume/{name}/{correlationId}")]
        public async Task<ActionResult<bool>> ResumeAsync(
            [FromServices] IWorkflowInstanceStore wfStore,
            [FromServices] IWorkflowInvoker wfInvoker,  
            string name,
            string correlationId,
            CancellationToken cancellationToken) {
            
            await wfInvoker.TriggerSignalAsync(name, correlationId: correlationId, cancellationToken: cancellationToken); // .ResumeAsync(wf, cancellationToken: cancellationToken);
            
            return Ok(true);
        }
    }
}