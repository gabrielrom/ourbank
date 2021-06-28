using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

using ourbank.Controllers;
using ourbank.entities;
using ourbank.Error;
using ourbank.Repositories;
using ourbank.Utils;

namespace ourbank.sevices {
  public class AuthenticateUserService {
    private IUsersRepository _usersRepository;
    private IConfiguration _configuration;

    public AuthenticateUserService(
      IUsersRepository usersRepository, 
      IConfiguration configuration
    ) {
      this._usersRepository = usersRepository;
      this._configuration = configuration;
    }

    public object execute(IRequest data) {
      User user = this._usersRepository.findByEmail(data.email);
      
      if (user == null) {
        throw new AppError("Email or password incorrect!", 401);
      }

      bool isPasswordMatch = BC.Verify(
        data.password, 
        user.password
      );

      if(!isPasswordMatch) {
        throw new AppError("Email or password incorrect!", 401);
      }

      var tokenService = new TokenService(
        this._configuration
      );

      string tokenJWT = tokenService.GenerateToken(user);

      return new {
        user = new {
          name = user.name,
          email = user.email,
          avatar = $"https://localhost:3333/avatar/{user.avatar_url}",
          account_id = user.accountId
        },
        token = tokenJWT
      };
    }

  } 
}