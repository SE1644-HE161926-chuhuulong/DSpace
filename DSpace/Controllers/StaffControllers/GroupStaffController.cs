using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers.StaffControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class GroupStaffController : ControllerBase
   {
      private GroupService _groupService;
      private GroupPeopleService _groupPeopleService;
      public GroupStaffController(GroupService groupService, GroupPeopleService groupPeopleService)
      {
         _groupService = groupService;
         _groupPeopleService = groupPeopleService;
      }
      [HttpGet("[action]")]
      public async Task<IActionResult> GetGroupByPeopleId()
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;

         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         var userId = int.Parse(idUser);
         var response = await _groupService.GetGroupByPeopleId(userId);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getGroup/{id}")]
      public async Task<IActionResult> GetGroupById(int id)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;

         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         var userId = int.Parse(idUser);

         var response = await _groupService.GetGroupByIDForStaff(id,userId);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
   }
}
