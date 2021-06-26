using ourbank.entities;
using ourbank.dtos;

namespace ourbank.Repositories {
  public interface ITransactionsRepository {
    Transaction create(CreateTransactionDTO data);
  }
}