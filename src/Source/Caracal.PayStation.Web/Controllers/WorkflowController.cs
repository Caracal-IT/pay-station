using System.Collections.Generic;
using System.Linq;
using Caracal.PayStation.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController: ControllerBase {
        private readonly IWebHostEnvironment _environment;
        private bool IsLoggedIn => !string.IsNullOrWhiteSpace(Cookies.GetUserToken(Request));
        
        public WorkflowController(IWebHostEnvironment environment) {
            _environment = environment;
        }

        [HttpGet("process/{name}")]
        public ActionResult GetProcess(string name) {
            var workflowDir =  _environment.ContentRootFileProvider.GetDirectoryContents("Workflow");
            var publicWf = new List<string>();
            publicWf.Add("default");
            publicWf.Add("login");
            publicWf.Add("logout");
            
            if (!IsLoggedIn && !publicWf.Contains(name.ToLower())) 
                name = "login";

            var process = workflowDir.FirstOrDefault(f => f.Name.Equals($"{name.ToLower()}.wf.json"));

            if (process == null)
                return NoContent();
            
            return new FileStreamResult(process.CreateReadStream(), "application/json");
        }
    }
}