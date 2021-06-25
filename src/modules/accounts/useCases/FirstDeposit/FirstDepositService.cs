using System;
using ourbank.Error;
using ourbank.Repositories;

namespace ourbank.sevices {
  public class FirstDepositService {  

    private IUsersRepository _usersRepository;
    private IAccountsRepository _accountsRepository;

    public FirstDepositService (
      IUsersRepository usersRepository,
      IAccountsRepository accountsRepository
    ) {
      _usersRepository = usersRepository;
      _accountsRepository = accountsRepository;
    }

    public void execute(string user_id, decimal deposit_value) {
      if (deposit_value < 0) {
        throw new AppError("You cannot deposit a negative value!");
      } else if (deposit_value > 5000) {
        throw new AppError("You cannot deposit an amount greater than 5000");
      }

      var userExists = _usersRepository.findById(user_id);

      if (userExists == null) {
        throw new AppError("This user does not exists!", 404);
      }

      var account = _accountsRepository.findById(userExists.accountId);

      if (!account.isFirstDeposit) {
        throw new AppError("You already made a first deposit!", 406);
      }

      _accountsRepository.addFirstDeposit(
        account, 
        deposit_value
      );
    }
  }
}