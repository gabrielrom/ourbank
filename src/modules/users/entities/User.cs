using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ourbank.entities{
  [Table("Users")]
  public class User : BaseEntity {
    [Key]
    public string id { get; private set; }

    [Column]
    [Required]
    public string name { get; set; }

    [Column]
    [Required]
    public string email { get; set; }

    [Column]
    [Required]
    public string password { get; set; }

    [Column]
    public string avatar_url { get; set; }

    [Column("account_id")]
    public string accountId { get; set; }
    
    public Account account { get; set; }

    public User() {
      if (String.IsNullOrEmpty(this.id)) {
        this.id = Convert.ToString(Guid.NewGuid());
      }
    }
  }
}