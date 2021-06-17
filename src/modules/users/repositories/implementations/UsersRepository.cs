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

      _repository.Users.Add(user);
      _repository.SaveChanges();

      return user;
    }

  }
}