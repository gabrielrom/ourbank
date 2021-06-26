using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ourbank.entities {
  public class Transaction : BaseEntity {
    [Key]
    public string id { get; private set; }
    
    [Column]
    [Required]
    public string sender_id { get; set; }

    [Column]
    [Required]
    public string recipient_id { get; set; }

    [Column]
    [Required]
    public string description { get; set; }

    [Column]
    [Required]
    public decimal value { get; set; }

    public Transaction() {
      if (String.IsNullOrEmpty(this.id)) {
        this.id = Convert.ToString(Guid.NewGuid());
      }
    }

  }
}