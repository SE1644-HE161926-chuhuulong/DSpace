using Domain;
using System.Security.Claims;

namespace DSpace.Services
{
   public interface JwtTokenService
   {
      public string GenerateToken(List<Claim> claims);

   }
}
