using System.Collections.Generic;
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

        public async Task<Withdrawal> AddWithdrawalAsync(Withdrawal withdrawal, CancellationToken cancellationToken) => await _repository.AddWithdrawalAsync(withdrawal, cancellationToken);

        public async Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses, CancellationToken token) => 
            await _repository.UpdateWithdrawalStatusAsync(statuses, token);

        public async Task<bool> UpdateWfUrlAsync(long withdrawalId, string url, CancellationToken token) =>
            await _repository.UpdateWfUrlAsync(withdrawalId, url, token);

        public async Task<Withdrawal> GetWithdrawal(long id, CancellationToken token) =>
            await _repository.GetWithdrawal(id, token);

        public async Task<bool> UpdateAmountAsync(long id, string amount, CancellationToken token) => 
            await _repository.UpdateAmountAsync(id, amount, token);
    }
}