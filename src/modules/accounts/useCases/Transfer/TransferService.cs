using ourbank.Error;
using ourbank.Repositories;
using ourbank.dtos;

namespace ourbank.sevices {
  public class TransferService {  

    private IAccountsRepository _accountsRepository;
    private IUsersRepository _usersRepository;

    public TransferService (
      IAccountsRepository accountsRepository,
      IUsersRepository usersRepository
    ) {
      
      _accountsRepository = accountsRepository;
      _usersRepository = usersRepository;
    }

    public object execute(string sender_id, TransferDTO transfer) {
      if (transfer.value < 0) {
        throw new AppError(
          "You cannot make a transfer with a negative amount!"
        );
      } else if (transfer.value == 0) {
        throw new AppError(
          "You cannot make a transfer with a zero amount!"
        );
      } 

      var senderUser = _usersRepository.findById(sender_id);

      if (senderUser == null) {
        throw new AppError("This user does not exists!", 404);
      }

      var accountSender = _accountsRepository.findById(senderUser.accountId);

      if (accountSender.balance < transfer.value) {
        throw new AppError(
          "You cannot transfer an amount greater than your balance!"
        );
      }

      var recipientAccount = _accountsRepository.findByAccountNumber(
        transfer.account_number
      );

      if (recipientAccount == null) {
        throw new AppError("This account does not exists!", 404);
      } else if (recipientAccount.id == accountSender.id) {
        throw new AppError("You cannot transfer for yourself!");
      }

      _accountsRepository.transfer(
        accountSender, 
        recipientAccount, 
        transfer.value
      );

      return new {
        message = "the transfer was successful!"
      };
    }
  }
}