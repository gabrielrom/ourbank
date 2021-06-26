namespace ourbank.dtos {
  public class CreateTransactionDTO {
    public string sender_id { get; set; }
    public string recipient_id { get; set; }
    public string description { get; set; }
    public decimal value { get; set; }
  }
}