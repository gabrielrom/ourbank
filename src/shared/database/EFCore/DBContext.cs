using Microsoft.EntityFrameworkCore;
using ourbank.entities;

namespace ourbank.DBContext {
  public class DBContext : DbContext {
    public DbSet<User> Users { get; set; }

    public DBContext (DbContextOptions<DBContext> options) : base(options) {}
  }
}