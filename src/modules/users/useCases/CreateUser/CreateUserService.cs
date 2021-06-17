using ourbank.entities;
using ourbank.Repositories;

namespace ourbank.sevices {
  public class CreateUserService {

    private IUsersRepository _usersRepository;

    public CreateUserService(IUsersRepository usersRepository) {
      this._usersRepository = usersRepository;
    }
    
    public User execute(ICreateDTO data) {
      User user = this._usersRepository.create(new ICreateDTO {
        name = data.name,
        email = data.email,
        password = data.password
      });

      return user;
    }

  }
}