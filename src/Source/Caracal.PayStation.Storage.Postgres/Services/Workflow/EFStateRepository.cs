using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Storage.Postgres.DbContexts;
using Caracal.PayStation.Workflow.Models.Withdrawals;
using Caracal.PayStation.Workflow.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Caracal.PayStation.Storage.Postgres.Services.Workflow {
    public class EFStateRepository: StateRepository {
        private readonly WithdrawalDbContext _dbContext;

        public EFStateRepository(WithdrawalDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses, CancellationToken token) {
            var results = new List<WithdrawalStatusUpdateResult>(); 
            foreach (var withdrawalStatus in statuses) 
                results.Add(await UpdateStatusAsync(withdrawalStatus, token));        
            
            await _dbContext.SaveChangesAsync(token);
            return results;
        }

        private async Task<WithdrawalStatusUpdateResult> UpdateStatusAsync(WithdrawalStatus withdrawalStatus, CancellationToken token) {
            var withdrawal = await _dbContext.Withdrawals.FirstOrDefaultAsync(w => w.Id == withdrawalStatus.WithdrawalId, token);

            if (withdrawal == null || string.IsNullOrWhiteSpace(withdrawalStatus.Status))
                return new WithdrawalStatusUpdateResult(withdrawalStatus, "Not Found");
            
            withdrawal.Status = withdrawalStatus.Status;
            return new WithdrawalStatusUpdateResult(withdrawalStatus, string.Empty, true);
        }
    }
}