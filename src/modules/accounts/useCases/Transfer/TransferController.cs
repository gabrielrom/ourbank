using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ourbank.Repositories;
using ourbank.sevices;
using System.Linq;
using System.Security.Claims;
using ourbank.dtos;

namespace ourbank.Controllers {
  public class RequestTransferBody {
    public string bank_branch { get; set; }
    public string account_number { get; set; }
    public string description { get; set; }
    public decimal value { get; set; }
  }

  [ApiController]
  [Route("/accounts/transfer")]
  public class TransferController : ControllerBase {

    private TransferService _transferService;

    public TransferController(
      IUsersRepository usersRepository,
      IAccountsRepository accountsRepository,
      ITransactionsRepository transactionsRepository
    ) {
      _transferService = new TransferService(
        accountsRepository,
        usersRepository,
        transactionsRepository
      );
    }

    [HttpPost]
    [Authorize]
    public ObjectResult handle([FromBody] RequestTransferBody request) {
      var user_id = User.Claims
      .FirstOrDefault(claim => claim.Type == ClaimTypes.PrimarySid)
      .Value.ToString();

      var result = _transferService.execute(user_id, new TransferDTO {
        account_number = request.account_number,
        bank_branch = request.bank_branch,
        description = request.description,
        value = request.value
      });

      return StatusCode(201, result);
    }
  }
}