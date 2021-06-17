namespace ourbank.entities {
  public class User {
    public string id { get; private set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string avatar_url { get; set; }
    public string account_id { get; set; }
  }
}