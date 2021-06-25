using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ourbank.entities {
  public class Account : BaseEntity {
    [Key]
    public string id { get; private set; }
    
    [Column]
    [Required]
    public string bank_branch { get; private set; }

    [Column]
    [Required]
    public string account_number { get; private set; }

    [Column]
    [Required]
    public decimal balance { get; set; }

    [Column]
    [Required]
    public bool isFirstDeposit { get; set; } = true;

    public Account() {
      if (String.IsNullOrEmpty(this.id)) {
        this.id = Convert.ToString(Guid.NewGuid());
      }
    }

    public void GenerateAccountNumber() {
      var random = new Random();

      string bank_branch = random.Next(0, 9999).ToString();
      string account_number = random.Next(100000000, 999999999).ToString();

      this.bank_branch = bank_branch;
      this.account_number = account_number;
    }

  }
}