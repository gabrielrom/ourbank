using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ourbank.entities {

  public class BaseEntity {
    [Column(TypeName = "timestamp")]
    public DateTime created_at { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime updated_at { get; set; }
  }
  
}