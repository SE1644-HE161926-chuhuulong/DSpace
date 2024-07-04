using Application.GroupPeoples;
using DSpace.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class GroupPeopleController : Controller
   {
      private GroupPeopleService _groupPeopleService;
      public GroupPeopleController(GroupPeopleService groupPeopleService)
      {
         _groupPeopleService = groupPeopleService;
      }
      [HttpPost("[action]")]
      public async Task<IActionResult> AddPeopleInGroup(GroupPeopleDTO groupPeopleDTO)
      {
         var response = await _groupPeopleService.AddPeopleInGroup(groupPeopleDTO);
         if (response.IsSuccess)
         {
            return Ok(response.Message);
         }
         else
         {
            return BadRequest(response.Message);
         }

      }
      

      [HttpPost("AddListPeopleInGroup")]
      public async Task<IActionResult> AddListPeopleInGroup(GroupPeopleListDTOForCreateUpdate groupPeopleDTO)
      {
         var response = await _groupPeopleService.AddListPeopleInGroup(groupPeopleDTO);
         if (response.IsSuccess)
         {
            return Ok(response.Message);
         }
         else
         {
            return BadRequest(response.Message);
         }

      }
      [HttpPut]
      [Route("[action]")]
      public async Task<IActionResult> UpdatePeopleInGroup(GroupPeopleListDTOForCreateUpdate groupPeopleDTO)
      {
         var response = await _groupPeopleService.DeleteListPeopleInGroup(groupPeopleDTO.GroupId);
         if (response.IsSuccess)
         {
            response = await _groupPeopleService.AddListPeopleInGroup(groupPeopleDTO);
            if (response.IsSuccess)
            {
               return Ok("update list success");
            }
            return Conflict(response.Message);

         }
         return Conflict(response.Message);
      }
      [HttpPut]
      [Route("[action]")]
      public async Task<IActionResult> DeleteListPeopleInGroup(GroupPeopleListDTOForCreateUpdate groupPeopleDTO)
      {
         var response = await _groupPeopleService.DeleteListPeopleInGroup(groupPeopleDTO.GroupId);
         if (response.IsSuccess)
         {
            return Ok("update list success");
         }
         return Conflict(response.Message);
      }
      
      [HttpPut]
      [Route("[action]")]
      public async Task<IActionResult> DeletePeopleInGroup(GroupPeopleDTO groupPeopleDTO)
      {
         var response = await _groupPeopleService.DeletePeopleInGroup(groupPeopleDTO);
         if (response.IsSuccess)
         {
            return Ok("update list success");
         }
         return Conflict(response.Message);
      }
   }
}
