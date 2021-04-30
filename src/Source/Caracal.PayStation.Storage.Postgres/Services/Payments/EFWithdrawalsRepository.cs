using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Payments.Models;
using Caracal.PayStation.Payments.Repositories;
using Caracal.PayStation.Storage.Postgres.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Caracal.PayStation.Storage.Postgres.Services.Payments {
    public class EFWithdrawalsRepository: WithdrawalsRepository {
        private readonly WithdrawalDbContext _dbContext;

        public EFWithdrawalsRepository(WithdrawalDbContext dbContext) =>
            _dbContext = dbContext;
        
        public async Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter filter, CancellationToken cancellationToken) {
            try {
                var items = await _dbContext.Withdrawals.OrderBy(i => i.Id).ToListAsync(cancellationToken);

                return new PagedData<Withdrawal> {
                    Items = items.Select(i => new Withdrawal(i.Id, i.Account, i.Amount, i.Status)).ToList(),
                    PageNumber = 0,
                    NumberOfResults = items.Count,
                    NumberOfRows = 10
                };
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Withdrawal> AddWithdrawalAsync(Withdrawal withdrawal, CancellationToken cancellationToken) {
            var item = new Model.Withdrawal {
                Account = withdrawal.Account,
                Amount = withdrawal.Amount,
                Status = string.Empty
            };
            
            _dbContext.Withdrawals.Add(item);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Withdrawal(item.Id, item.Account, item.Amount, item.Status);
        }

        public async Task<bool> UpdateWfUrlAsync(long withdrawalId, string url,  CancellationToken token) {
            var withdrawal = await _dbContext.Withdrawals.FirstOrDefaultAsync(w => w.Id == withdrawalId, token);

            if (withdrawal == null)
                return false;
            
            withdrawal.WorkflowUrl = url;

            return await _dbContext.SaveChangesAsync(token) > 0;
        }

        public async Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses,
            CancellationToken token) {
            var results = new List<WithdrawalStatusUpdateResult>();
            foreach (var withdrawalStatus in statuses)
                results.Add(await UpdateStatusAsync(withdrawalStatus));

            await _dbContext.SaveChangesAsync(token);
            return results;

            async Task<WithdrawalStatusUpdateResult> UpdateStatusAsync(WithdrawalStatus withdrawalStatus) {
                var withdrawal = await _dbContext.Withdrawals.FirstOrDefaultAsync(w => w.Id == withdrawalStatus.WithdrawalId, token);

                if (withdrawal == null || string.IsNullOrWhiteSpace(withdrawalStatus.Status))
                    return new WithdrawalStatusUpdateResult(withdrawalStatus, "Not Found");

                withdrawal.Status = withdrawalStatus.Status;
                return new WithdrawalStatusUpdateResult(withdrawalStatus, string.Empty, true);
            }
        }
    }
}