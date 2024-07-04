using Application.Groups;
using DSpace.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class GroupController : Controller
   {
      private GroupService _groupService;
      private GroupPeopleService _groupPeopleService;
      public GroupController(GroupService groupService, GroupPeopleService groupPeopleService)
      {
         _groupService = groupService;
         _groupPeopleService = groupPeopleService;
      }
      [HttpPost("createGroup")]
      public async Task<IActionResult> CreateGroup(GroupDTOForCreateUpdate groupDTO)
      {
         var response = await _groupService.CreateGroup(groupDTO);
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
         var response = await _groupService.GetGroupByID(id);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getGroupByTitle/{title}")]
      public async Task<IActionResult> GetGroupByTitle(string title)
      {
         var response = await _groupService.GetGroupByTitle(title);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getListOfGroup")]
      public async Task<IActionResult> GetAllGroup()
      {
         var response = await _groupService.GetAllGroup();
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpPut("updateGroup/{groupId}")]
      public async Task<IActionResult> UpdateGroup(int groupId, GroupDTOForCreateUpdate groupDTO)
      {
         var response = await _groupService.UpdateGroup(groupId, groupDTO);
         if (response.IsSuccess)
         {
            return Ok(response.Message);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpDelete("DeleteGroup/{groupId}")]
      public async Task<IActionResult> DeleteGroup(int groupId)
      {
         var response = await _groupService.DeleteGroup(groupId);
         if (response.IsSuccess)
         {
            return Ok(response.Message);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
   }
}
