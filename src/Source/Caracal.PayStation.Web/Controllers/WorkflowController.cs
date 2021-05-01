using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController: ControllerBase {
        private readonly IWebHostEnvironment _environment;

        public WorkflowController(IWebHostEnvironment environment) => 
            _environment = environment;

        [HttpGet("process/{name}")]
        [ResponseCache(Location = ResponseCacheLocation.Any, VaryByHeader = "X-Version", Duration = 120)]
        public ActionResult GetProcess(string name) {
            var workflowDir =  _environment.ContentRootFileProvider.GetDirectoryContents("Workflow");
            var process = workflowDir.FirstOrDefault(f => f.Name.Equals($"{name.ToLower()}.wf.json"));

            if (process == null)
                return NoContent();
            
            return new FileStreamResult(process.CreateReadStream(), "application/json");
        }
    }
}