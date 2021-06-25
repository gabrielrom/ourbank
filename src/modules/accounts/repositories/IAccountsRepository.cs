using ourbank.entities;

namespace ourbank.Repositories {
  public interface IAccountsRepository {
    Account create();
    Account findById(string account_id);
    Account findByAccountNumber(string account_number);
    void addFirstDeposit(Account account, decimal deposit_value);
    void transfer(
      Account accountSender, 
      Account accountRecipient,
      decimal value
    );
  }
}