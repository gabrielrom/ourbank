using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ourbank.entities;
using ourbank.Repositories;
using ourbank.sevices;
using System;
using System.Linq;
using System.Security.Claims;

namespace ourbank.Controllers {
  public class BodyValue {
    public decimal value { get; set; }
  }

  [ApiController]
  [Route("/accounts/firstdeposit")]
  public class FirstDepositController : ControllerBase {

    private FirstDepositService _firstDepositService;

    public FirstDepositController(
      IUsersRepository usersRepository,
      IAccountsRepository accountsRepository
    ) {
      _firstDepositService = new FirstDepositService(
        usersRepository, 
        accountsRepository
      );
    }

    [HttpPost]
    [Authorize]
    public ObjectResult handle([FromBody] BodyValue request) {
      var user_id = User.Claims
      .FirstOrDefault(claim => claim.Type == ClaimTypes.PrimarySid)
      .Value.ToString();

      _firstDepositService.execute(user_id, request.value);

      return StatusCode(200, new {
        message = $"The amount of {request.value} reais was deposited!"
      });
    }
  }
}