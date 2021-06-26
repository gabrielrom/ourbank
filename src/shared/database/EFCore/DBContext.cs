using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ourbank.entities;

namespace ourbank.DBContext {
  public class DBContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DBContext (DbContextOptions<DBContext> options) : base(options) {}

    public override int SaveChanges(bool acceptAllChangesOnSuccess) {
      OnBeforeSaving();
      return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(
      bool acceptAllChangesOnSuccess, 
      CancellationToken cancellationToken = default(CancellationToken)
    ) {
      OnBeforeSaving();
      return (await base.SaveChangesAsync(
        acceptAllChangesOnSuccess, 
        cancellationToken
      ));
    }

    private void OnBeforeSaving() {
      var entries = ChangeTracker.Entries();
      var utcNow = DateTime.UtcNow;

      foreach (var entry in entries) {
        if (entry.Entity is BaseEntity trackable) {
          switch (entry.State) {
            case EntityState.Modified:
              
              trackable.updated_at = utcNow;
              entry.Property("created_at").IsModified = false;
              break;

            case EntityState.Added:
              trackable.created_at = utcNow;
              trackable.updated_at = utcNow;
              break;
          }
        }
      }
    }
  }
}