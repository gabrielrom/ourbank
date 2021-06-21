using Microsoft.AspNetCore.Mvc;
using ourbank.entities;
using ourbank.Repositories;
using ourbank.sevices;

namespace ourbank.Controllers {
  public class RequestBody {
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
  }

  [ApiController]
  [Route("/users")]
  public class CreateUserController : ControllerBase {

    private CreateUserService _createUserService;

    public CreateUserController(
      IUsersRepository usersRepository,
      IAccountsRepository accountsRepository
    ) {
      this._createUserService = new CreateUserService(
        usersRepository, 
        accountsRepository
      );
    }

    [HttpPost]
    public ObjectResult handle([FromBody] RequestBody request) {
      User user = this._createUserService.execute(new ICreateDTO {
        name = request.name,
        email = request.email,
        password = request.password
      });

      return StatusCode(201, new {
        name = user.name,
        email = user.email,
        avatar_url = user.avatar_url,
        account_id = user.account_id,
        created_at = user.created_at,
        updated_at = user.updated_at,
      });
    }
  }
}