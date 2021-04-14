using System.Threading.Tasks;
using Caracal.PayStation.PaymentEngine;
using Caracal.PayStation.WorkflowEngine;

namespace Caracal.PayStation.Application {
    public class Start {
        private WithdrawalEngine _withdrawalEngine;
        private ChangeStateEngine _changeStateEngine;
        
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