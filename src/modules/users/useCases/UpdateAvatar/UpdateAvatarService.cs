using ourbank.Repositories;
using ourbank.Utils;
using Microsoft.AspNetCore.Hosting;

namespace ourbank.sevices {
  public class UpdateAvatarService {

    private IUsersRepository _usersRepository;
    private IWebHostEnvironment _enviroment;

    public UpdateAvatarService(
      IUsersRepository usersRepository,
      IWebHostEnvironment environment
    ) {
      _usersRepository = usersRepository;
      _enviroment = environment;
    }
    
    public void execute(string user_id, string avatar_url) {
      var user = _usersRepository.findById(user_id);

      if (user.avatar_url != null) {
        HandleFiles.deleteFile(
          $"{_enviroment.ContentRootPath}/tmp/avatars/{user.avatar_url}"
        );
      }

      user.avatar_url = avatar_url;

      _usersRepository.save();
    }

  }
}