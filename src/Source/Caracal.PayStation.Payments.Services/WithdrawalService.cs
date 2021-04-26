using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Payments.Models;
using Caracal.PayStation.Payments.Repositories;
using Base = Caracal.PayStation.Payments;

namespace Caracal.PayStation.Payments.Services {
    public class WithdrawalService: Base.WithdrawalService {
        private readonly WithdrawalsRepository _repository;

        public WithdrawalService(WithdrawalsRepository repository) =>
            _repository = repository;
        
        public async Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter filter, CancellationToken cancellationToken) => 
            await _repository.GetWithdrawalsAsync(filter, cancellationToken);
    }
}