using System.Threading.Tasks;
using Caracal.PayStation.Payments;
using Caracal.PayStation.Workflow;

namespace Caracal.PayStation.Application {
    public class Start {
        private readonly WithdrawalEngine _withdrawalEngine;
        private readonly ChangeStateEngine _changeStateEngine;
        
        public Start(WithdrawalEngine withdrawalEngine, ChangeStateEngine changeStateEngine) {
            _withdrawalEngine = withdrawalEngine;
            _changeStateEngine = changeStateEngine;
        }

        public async Task Initialize() {
            await _withdrawalEngine.RegisterEventListener();
            await _changeStateEngine.RegisterEventListener();
        }
    }
}