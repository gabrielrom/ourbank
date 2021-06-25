using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ourbank.entities;

namespace ourbank.Utils {
  public class TokenService {
    private IConfiguration _configuration { get; }

    public TokenService(IConfiguration configuration) {
      this._configuration = configuration;
    }


    public string GenerateToken(User user) {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(
        _configuration.GetConnectionString("SecretToken")
      );

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new Claim[] {
          new Claim(ClaimTypes.PrimarySid, user.id),
        }),

        Expires = DateTime.UtcNow.AddHours(2),

        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }

  }
}