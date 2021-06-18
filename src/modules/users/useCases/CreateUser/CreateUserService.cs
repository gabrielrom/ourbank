using ourbank.entities;
using ourbank.Repositories;
using ourbank.Error;
using BC = BCrypt.Net.BCrypt;

namespace ourbank.sevices {
  public class CreateUserService {

    private IUsersRepository _usersRepository;

    public CreateUserService(IUsersRepository usersRepository) {
      this._usersRepository = usersRepository;
    }
    
    public User execute(ICreateDTO data) {
      if (
        string.IsNullOrEmpty(data.name) || 
        string.IsNullOrEmpty(data.email) || 
        string.IsNullOrEmpty(data.password)
      ) {
        throw new AppError(
          "The properties name, email and password is required!"
        );
      }

      User userAlreadyExists = this._usersRepository.findByEmail(
        data.email
      );

      if (userAlreadyExists != null) {
        throw new AppError("this email is already exits!");
      }

      string hashedPassword = BC.HashPassword(data.password);

      User user = this._usersRepository.create(new ICreateDTO {
        name = data.name,
        email = data.email,
        password = hashedPassword
      });

      return user;
    }

  }
}