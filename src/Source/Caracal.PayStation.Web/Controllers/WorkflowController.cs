using System.Collections.Generic;
using System.Linq;
using Caracal.PayStation.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController: ControllerBase {
        private static readonly List<string> PublicWf = new() {"default", "login", "logout", "home"};
        private bool IsLoggedIn => !string.IsNullOrWhiteSpace(Cookies.GetUserToken(Request));
        private readonly IWebHostEnvironment _environment;
        
        public WorkflowController(IWebHostEnvironment environment) => 
            _environment = environment;

        [HttpGet("process/{name}")]
        [ResponseCache(Location = ResponseCacheLocation.Client, VaryByHeader = "X-UserToken", Duration = 1500000)]
        public ActionResult GetProcess(string name) {
            var workflowDir =  _environment.ContentRootFileProvider.GetDirectoryContents("Workflow");

            if (!IsLoggedIn && !PublicWf.Contains(name.ToLower())) 
                name = "login";

            var process = workflowDir.FirstOrDefault(f => f.Name.Equals($"{name.ToLower()}.wf.json"));

            if (process == null)
                return NoContent();
            
            return new FileStreamResult(process.CreateReadStream(), "application/json");
        }
    }
}