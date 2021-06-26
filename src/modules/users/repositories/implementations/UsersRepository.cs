using System.Linq;
using ourbank.entities;
using Microsoft.AspNetCore.Mvc;

namespace ourbank.Repositories {
  public class UsersRepository : IUsersRepository {
    private DBContext.DBContext _repository;

    public UsersRepository([FromServices] DBContext.DBContext context) {
      this._repository = context;
    }

    public User create(ICreateDTO data) {
      User user = new User();

      user.name = data.name;
      user.email = data.email;
      user.password = data.password;
      user.accountId = data.account_id;

      _repository.Users.Add(user);
      _repository.SaveChanges();

      return user;
    }

    public User findByEmail(string email) {
      User user = _repository.Users.FirstOrDefault(user => 
        user.email == email
      );

      return user;
    }

    public User findById(string user_id) {
      User user = _repository.Users.Find(user_id);

      return user;
    }

    public User findByAccountId(string account_id) {
      User user = _repository.Users.FirstOrDefault(user => 
        user.accountId == account_id
      );

      return user;
    }
  }
}