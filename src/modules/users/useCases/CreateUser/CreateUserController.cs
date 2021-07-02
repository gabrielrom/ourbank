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
    public StatusCodeResult handle([FromBody] RequestBody request) {
      this._createUserService.execute(new() {
        name = request.name,
        email = request.email,
        password = request.password
      });

      return StatusCode(201);
    }
  }
}