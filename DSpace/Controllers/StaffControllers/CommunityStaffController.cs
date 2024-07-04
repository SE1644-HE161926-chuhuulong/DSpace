using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers.StaffControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CommunityStaffController : ControllerBase
   {
      private CommunityService _communityService;

      public CommunityStaffController(CommunityService communityService)
      {
         _communityService = communityService;
      }
      [HttpGet("[action]")]
      public async Task<IActionResult> GetAllCommunities()
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;

         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         var userId = int.Parse(idUser);
         var result = await _communityService.GetCommunityByPeopleId(userId);
         if (result.IsSuccess)
         {
            if (result.ObjectResponse != null) {
               return Ok(result.ObjectResponse);
            }
            return NotFound(result.Message);

         }
         else { 
            return BadRequest(result.Message);
         }

      }
   }
}
