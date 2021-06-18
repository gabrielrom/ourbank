using ourbank.entities;

namespace ourbank.Repositories {
  public class ICreateDTO {
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
  }

  public interface IUsersRepository {
    User create(ICreateDTO data);
    User findByEmail(string email);
  }
}