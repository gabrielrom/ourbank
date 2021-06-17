using Microsoft.EntityFrameworkCore;

namespace ourbank.DBContext {
  public class DBContext : DbContext {
    public DBContext (DbContextOptions<DBContext> options) : base(options) {}
  }
}