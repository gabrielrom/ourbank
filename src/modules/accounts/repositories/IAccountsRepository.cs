using ourbank.entities;

namespace ourbank.Repositories {
  public interface IAccountsRepository {
    Account create();
    Account findById(string account_id);
    void addFirstDeposit(Account account, decimal deposit_value);
  }
}