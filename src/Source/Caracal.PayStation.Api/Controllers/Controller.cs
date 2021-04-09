using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers {
    public abstract class Controller: ControllerBase {
        protected async Task<ActionResult<T>> TryExecute<T>(Func<Task<ActionResult<T>>> work)  {
            try { return await work(); }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch { return Problem(); }
        }
    }
}