using Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DSpace.Services
{
   public interface UserService
   {
      public User CreateInstanceUser();
      public Task<string> GetRole(User user);
      public Task<bool> AssignRole(User user,string role);

      public Task<bool> CreateUser(User user);

      public Task<User> getUserByEmail(string email);

      public List<Claim> getUserClaim(User user, string roles);

      public Task<bool> AddLoginAsync(User user,ExternalLoginInfo info);
      public Task<User> getUserById(string id);

      public Task<bool> UpdateUser(User user);

      



   }
}
