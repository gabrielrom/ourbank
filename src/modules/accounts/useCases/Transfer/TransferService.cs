using ourbank.Error;
using ourbank.Repositories;
using ourbank.dtos;

namespace ourbank.sevices {
  public class TransferService {  

    private IAccountsRepository _accountsRepository;
    private IUsersRepository _usersRepository;
    private ITransactionsRepository _transactionsRepository;

    public TransferService (
      IAccountsRepository accountsRepository,
      IUsersRepository usersRepository,
      ITransactionsRepository transactionsRepository
    ) {
      
      _accountsRepository = accountsRepository;
      _usersRepository = usersRepository;
      _transactionsRepository = transactionsRepository;
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

      var recipientUser = _usersRepository.findByAccountId(
        recipientAccount.id
      );

      _transactionsRepository.create(new CreateTransactionDTO{
        sender_id = sender_id,
        recipient_id = recipientUser.id,
        description = transfer.description,
        value = transfer.value
      });

      return new {
        message = "the transfer was successful!"
      };
    }
  }
}