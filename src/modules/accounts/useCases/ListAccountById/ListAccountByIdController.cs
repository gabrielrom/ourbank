using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ourbank.Repositories;

namespace ourbank.sevices
{

  [ApiController]
  [Route("/accounts")]
  public class ListAccountByIdController : ControllerBase {

    private ListAccountByIdService _listAccountByIdService;

    public ListAccountByIdController(
      IAccountsRepository accountsRepository
    ) {
      _listAccountByIdService = new ListAccountByIdService(
        accountsRepository
      );
    }

    [HttpGet("{id}")]
    [Authorize]
    public ObjectResult handle(string id) {
      var account = _listAccountByIdService.execute(
        account_id: id
      );

      return StatusCode(200, account);
    }
  }
}