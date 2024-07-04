using Application.CommunityGroups;
using DSpace.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CommunityGroupController : Controller
   {
      private CommunityGroupService _communityGroupService;
      public CommunityGroupController(CommunityGroupService communityGroupService) {
         _communityGroupService= communityGroupService;
      }
      [HttpPost("[action]")]
      public async Task<IActionResult> AddCommunityGroup(CommunityGroupDTOForCreate communityGroupDTO)
      {
         var result = await _communityGroupService.AddCommunityManageGroup(communityGroupDTO);
         if (result.IsSuccess) {
            return Ok(result.Message);
         }
         
         return BadRequest(result.Message);
         
      }
      [HttpDelete("[action]")]
      public async Task<IActionResult> DeleteCommunityGroup(int id)
      {
         var result = await _communityGroupService.DeleteCommunityManageInGroup(id);
         if (result.IsSuccess)
         {
            return Ok(result.Message);
         }

         return BadRequest(result.Message);

      }
      [HttpPut("[action]")]
      public async Task<IActionResult> UpdateCommunityGroup(CommunityGroupDTOForUpdate communityGroupDTO)
      {
         var result = await _communityGroupService.UpdateCommunityManageGroup(communityGroupDTO);
         if (result.IsSuccess)
         {
            return Ok(result.Message);
         }

         return BadRequest(result.Message);

      }
      [HttpGet("[action]")]
      public async Task<IActionResult> GetAllCommunityGroup()
      {
         var result = await _communityGroupService.GetListCommunityGroup();
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }

         return BadRequest(result.Message);

      }
      
   }
}
