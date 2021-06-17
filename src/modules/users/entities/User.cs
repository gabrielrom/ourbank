using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ourbank.entities {
  [Table("Users")]
  public class User {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    [Column]
    public string account_id { get; set; }

    [Column(TypeName = "timestamp")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime created_at { get; set; }
    
    [Column(TypeName = "timestamp")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime updated_at { get; set; }
    public User() {
      if (String.IsNullOrEmpty(this.id)) {
        this.id = Convert.ToString(Guid.NewGuid());
      }
    }
  }
}