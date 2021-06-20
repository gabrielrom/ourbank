using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ourbank.Repositories;
using ourbank.sevices;

namespace ourbank.Controllers {
  public class IRequest {
    public string email { get; set; }
    public string password { get; set; }
  }

  [ApiController]
  [Route("/sessions")]
  public class AuthenticateUserController : ControllerBase {

    private AuthenticateUserService _authenticateUserService;

    public AuthenticateUserController (
      IUsersRepository usersRepository,
      IConfiguration configuration
    ) {
      this._authenticateUserService = new AuthenticateUserService(
        usersRepository, 
        configuration
      );
    }

    [HttpPost]
    public ObjectResult handle([FromBody] IRequest request) {
      object tokenInfo = this._authenticateUserService.execute(request);

      return StatusCode(200, tokenInfo);
    }

  }
}