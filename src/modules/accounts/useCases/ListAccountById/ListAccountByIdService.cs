using ourbank.Repositories;
using ourbank.entities;
using ourbank.Error;

namespace ourbank.sevices
{
  public class ListAccountByIdService {

    private IAccountsRepository _accountsRepository;

    public ListAccountByIdService(
      IAccountsRepository accountsRepository
    ) {
      _accountsRepository = accountsRepository;
    }

    public Account execute(string account_id) {
      var accountExists = _accountsRepository.findById(account_id);

      if (accountExists == null) {
        throw new AppError("This account does not exists!", 404);
      }

      return accountExists;
    }
  }
}