using ourbank.Repositories;
using ourbank.entities;
using System.Collections.Generic;
using ourbank.Error;

namespace ourbank.sevices {
  public class ListTransactionsService {

    private ITransactionsRepository _transactionsRepository;
    private IUsersRepository _usersRepository;

    public ListTransactionsService(
      ITransactionsRepository transactionsRepository,
      IUsersRepository usersRepository
    ) {
      _transactionsRepository = transactionsRepository;
      _usersRepository = usersRepository;
    }

    public List<Transaction> execute(
      string user_id, 
      int max_transactions
    ) {
      var userExists = _usersRepository.findById(user_id);

      if (userExists == null) {
        throw new AppError("This user does not exists!");
      }

      var transactions = _transactionsRepository.findById(
        user_id: userExists.id,
        number_max: max_transactions
      );

      return transactions;
    }
  }
}