using ourbank.entities;
using ourbank.dtos;
using System.Collections.Generic;

namespace ourbank.Repositories {
  public interface ITransactionsRepository {
    Transaction create(CreateTransactionDTO data);
    List<Transaction> findById(string user_id, int number_max);
  }
}