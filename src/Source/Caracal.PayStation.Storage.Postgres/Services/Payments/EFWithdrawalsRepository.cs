using System;
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
    }
}