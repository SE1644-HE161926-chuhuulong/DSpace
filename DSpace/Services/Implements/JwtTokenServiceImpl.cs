using Domain;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace DSpace.Services.Implements
{
   public class JwtTokenServiceImpl : JwtTokenService
   {
      public IConfiguration _configuration;
      public string _jwtKey;
      public string _jwtAudience;
      public string _jwtIssuer;
      public int _jwtExpires;



      public JwtTokenServiceImpl(IConfiguration configuration, UserService userService)
      {
         _configuration = configuration;
         _jwtKey = _configuration["JWT:Secret"];
         _jwtAudience = _configuration["JWT:ValidAudience"];
         _jwtIssuer = _configuration["JWT:ValidIssuer"];
         _jwtExpires = Int32.Parse(_configuration["JWT:Expires"]);
         
      }

      public string GenerateToken(List<Claim> claims)
      {
         var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
         var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken(_jwtIssuer,
           _jwtAudience,
           claims,
           expires: DateTime.Now.AddMinutes(_jwtExpires),
           signingCredentials: credentials);

         return new JwtSecurityTokenHandler().WriteToken(token);
      }
   }
}
