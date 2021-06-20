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
  }
}