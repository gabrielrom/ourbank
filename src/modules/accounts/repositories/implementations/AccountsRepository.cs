using ourbank.entities;
using Microsoft.AspNetCore.Mvc;

namespace ourbank.Repositories {
  public class AccountsRepository : IAccountsRepository {
    private DBContext.DBContext _repository;

    public AccountsRepository([FromServices] DBContext.DBContext context) {
      this._repository = context;
    }

    public Account create() {
      Account account = new Account();

      account.GenerateAccountNumber();

      _repository.Accounts.Add(account);
      _repository.SaveChanges();

      return account;

    }

    public Account findById(string account_id) {
      Account account = _repository.Accounts.Find(account_id);

      return account;
    }

    public void addFirstDeposit(Account account, decimal deposit_value) {
      account.balance += deposit_value;
      account.isFirstDeposit = false;

      _repository.Update(account);
      _repository.SaveChanges();
    }

  }
}