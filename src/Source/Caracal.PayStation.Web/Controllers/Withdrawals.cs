using System.Linq;
using Caracal.PayStation.Web.ViewModel.Security.Withdrawals;
using Caracal.PayStation.Web.ViewModel.Security.Withdrawals.WithdrawalSearch;
using Microsoft.AspNetCore.Mvc;

namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class Withdrawals : ControllerBase {
        [HttpGet]
        public WithdrawalSearchResponseViewModel Get() {
            var response = new WithdrawalSearchResponseViewModel();
            
            foreach (var i in Enumerable.Range(1, 5)) 
                response.Withdrawals.Add(new WithdrawalViewModel(i, $"account {i}", $"R {i}0.44", "Requested"));
            
            return response;
        }
    }
}