using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace DSpace.Services.Implements
{
   public class UserServiceImpl : UserService
   {
      private readonly IMapper _mapper;

      private UserManager<User> _userManager;
      private RoleManager<Role> _roleManager;
      private readonly DSpaceDbContext _context;
      private readonly SignInManager<User> _signInManager;

      public UserServiceImpl(IMapper mapper, UserManager<User> userManager, DSpaceDbContext context, SignInManager<User> signInManager, RoleManager<Role> roleManager)
      {
         _mapper = mapper;
         _userManager = userManager;
         _context = context;
         _signInManager = signInManager;
         _roleManager = roleManager;
      }

      public async Task<bool> AssignRole(User user, string role)
      {
         try
         {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles != null) { 
               var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, roles);
               if (!removeRoleResult.Succeeded)
               {
                  throw new Exception();
               }
            }

            
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
         }
         catch (Exception ex)
         {

            return false;
         }
      }

      public User CreateInstanceUser()
      {
         try
         {
            return Activator.CreateInstance<User>();
         }
         catch
         {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. ");
         }
      }

      public async Task<string> GetRole(User user)
      {
         try { 
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.ToArray();
            return role[0];
         } 
         catch (Exception ex) {
            return null;
         }
      }
      public async Task<bool> CreateUser(User user)
      {
         try
         {
            var result = await _userManager.CreateAsync(user);
            return result.Succeeded;
         }
         catch (Exception ex)
         {
            return false;


         }
      }
      public async Task<bool> UpdateUser(User user)
      {
         try
         {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
         }
         catch (Exception ex)
         {
            return false;

         }
      }
      public async Task<User> getUserByEmail(string email)
      {
         try
         {
            var result = await _userManager.Users.Include(x=>x.People).FirstOrDefaultAsync(x => x.Email.Equals(email));
            return result;
         }
         catch(Exception ex) {
            return null;
         }
      }
      public async Task<User> getUserById(string id)
      {
         try
         {
            var result = await _userManager.FindByIdAsync(id);
            return result;
         }
         catch (Exception ex)
         {
            return null;
         }
      }

      public List<Claim> getUserClaim(User user,string role)
      {

         List<Claim> listClaims = new List<Claim>();
         listClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
         listClaims.Add(new Claim(ClaimTypes.Email, user.Email));
         listClaims.Add(new Claim(ClaimTypes.Name, user.FirstName +user.LastName));
         listClaims.Add(new Claim(ClaimTypes.Role, role));
         if ((role.Equals("ADMIN") || role.Equals("STAFF"))&&user.People!=null) {
            listClaims.Add(new Claim(ClaimTypes.Authentication, user.People.PeopleId.ToString()));
            
         }
         
         //listClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
         return listClaims;

      }
      public async Task<bool> AddLoginAsync(User user,ExternalLoginInfo info)
      {
         try
         {
            //var info = await _signInManager.GetExternalLoginInfoAsync();
            var result = await _userManager.AddLoginAsync(user,info);
            return result.Succeeded;
         }
         catch {
            return false;
         }
      
      }
   }
}
