using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ourbank.Repositories;
using System.Linq;
using System.Security.Claims;

namespace ourbank.sevices {
  
  [ApiController]
  [Route("/accounts/transactions")]
  public class ListTransactionsController : ControllerBase {

    private ListTransactionsService _listTransactionsService;

    public ListTransactionsController(
      ITransactionsRepository transactionsRepository,
      IUsersRepository usersRepository
    ) {
      _listTransactionsService = new ListTransactionsService(
        transactionsRepository,
        usersRepository
      );
    }

    [HttpGet]
    [Authorize]
    public ObjectResult handle([FromQuery(Name = "max")] int max_transactions) {
      var user_id = User.Claims
      .FirstOrDefault(claim => claim.Type == ClaimTypes.PrimarySid)
      .Value.ToString();


      var result = _listTransactionsService.execute(
        user_id,
        max_transactions
      );


      return StatusCode(200, result);
    }
  }
}