using ourbank.entities;
using ourbank.Repositories;
using ourbank.Error;
using BC = BCrypt.Net.BCrypt;

namespace ourbank.sevices {
  public class CreateUserService {

    private IUsersRepository _usersRepository;
    private IAccountsRepository _accountsRepository;


    public CreateUserService(
      IUsersRepository usersRepository, 
      IAccountsRepository accountsRepository
    ) {
      this._usersRepository = usersRepository;
      this._accountsRepository = accountsRepository;
    }
    
    public void execute(ICreateDTO data) {
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

      Account account = this._accountsRepository.create();

      User user = this._usersRepository.create(new ICreateDTO {
        name = data.name,
        email = data.email,
        password = hashedPassword,
        account_id = account.id
      });
    }

  }
}