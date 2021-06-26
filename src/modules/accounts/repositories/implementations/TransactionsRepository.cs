using ourbank.entities;
using Microsoft.AspNetCore.Mvc;
using ourbank.dtos;

namespace ourbank.Repositories {
  public class TransactionsRepository : ITransactionsRepository {
    private DBContext.DBContext _repository;

    public TransactionsRepository([FromServices] DBContext.DBContext context) {
      this._repository = context;
    }

    public Transaction create(CreateTransactionDTO data) {
      Transaction transaction = new Transaction {
        sender_id = data.sender_id,
        recipient_id = data.recipient_id,
        description = data.description,
        value = data.value
      };

      _repository.Transactions.Add(transaction);
      _repository.SaveChanges();


      return transaction;
    }
  }
}