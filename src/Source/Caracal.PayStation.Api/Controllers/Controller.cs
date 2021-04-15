using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Api.Controllers {
    public abstract class Controller: ControllerBase {
        protected async Task<ActionResult<T>> TryExecute<T>(Func<CancellationToken, Task<ActionResult<T>>> workAsync, CancellationToken cancellationToken)  {
            try { return await workAsync(cancellationToken); }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch { return Problem(); }
        }
    }
}