using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Caracal.PayStation.Storage.Postgres.Model;

namespace Caracal.PayStation.Storage.Postgres.DbContexts {
    public class WithdrawalDbContext: DbContext {
        public WithdrawalDbContext(DbContextOptions<WithdrawalDbContext> options) : base(options) { }
        
        public DbSet<Withdrawal> Withdrawals { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("paystore");

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            ChangeTracker.DetectChanges();
            
            return await base.SaveChangesAsync(token);
        }

    }
}

//dotnet ef migrations add InitialCreate -s ../Caracal.PayStation.Api/Caracal.PayStation.Api.csproj