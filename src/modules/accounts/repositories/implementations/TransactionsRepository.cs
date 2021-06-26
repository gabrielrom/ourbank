using ourbank.entities;
using Microsoft.AspNetCore.Mvc;
using ourbank.dtos;
using System.Collections.Generic;
using System.Linq;

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

    public List<Transaction> findById(
      string user_id, 
      int number_max = 0
    ) {
      
      List<Transaction> transactions = _repository
      .Transactions.Where(transaction => 
        transaction.sender_id == user_id 
        || transaction.recipient_id == user_id
      )
      .OrderByDescending(transaction => transaction.created_at)
      .ToList();

      if (number_max != 0) {
        transactions = transactions.GetRange(0, number_max);
      }

      return transactions;
    }
  }
}