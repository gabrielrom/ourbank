using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ourbank.Repositories;
using ourbank.sevices;
using System.Linq;
using System.Security.Claims;

namespace ourbank.Controllers {
  [ApiController]
  [Route("/users/avatar")]
  public class UpdateAvatarController : ControllerBase {

    private UpdateAvatarService _updateAvatarService;

    public UpdateAvatarController(
      IUsersRepository usersRepository,
      IWebHostEnvironment enviroment
      
    ) {
      _updateAvatarService = new UpdateAvatarService(
        usersRepository,
        enviroment
      );
    }

    [HttpPatch]
    [Authorize]
    public StatusCodeResult handle([FromForm] IFormFile file) {
      var user_id = User.Claims
      .FirstOrDefault(claim => claim.Type == ClaimTypes.PrimarySid)
      .Value.ToString();

      _updateAvatarService.execute(
        user_id, 
        HttpContext.Items.FirstOrDefault(
          item => item.Key.ToString() == "avatar_url"
        ).Value.ToString()
      );

      return StatusCode(204);
    }
  }
}