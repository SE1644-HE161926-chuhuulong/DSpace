using Application.CollectionGroups;
using Application.CollectionGroups;
using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CollectionGroupController : ControllerBase
   {
      private CollectionGroupService _collectionGroupService;
      public CollectionGroupController(CollectionGroupService collectionGroupService)
      {
         _collectionGroupService = collectionGroupService;
      }
      [HttpPost("[action]")]
      public async Task<IActionResult> AddCollectionGroup(CollectionGroupDTOForCreate collectionGroupDTO)
      {
         var result = await _collectionGroupService.AddCollectionManageGroup(collectionGroupDTO);
         if (result.IsSuccess)
         {
            return Ok(result.Message);
         }

         return BadRequest(result.Message);

      }
      [HttpPut("[action]")]
      public async Task<IActionResult> UpdateCollectionGroup(CollectionGroupDTOForUpdate collectionGroupDTO)
      {
         var result = await _collectionGroupService.UpdateCollectionManageGroup(collectionGroupDTO);
         if (result.IsSuccess)
         {
            return Ok(result.Message);
         }

         return BadRequest(result.Message);

      }
      [HttpDelete("[action]")]
      public async Task<IActionResult> DeleteCollectionGroup(int id)
      {
         var result = await _collectionGroupService.DeleteCollectionManageInGroup(id);
         if (result.IsSuccess)
         {
            return Ok(result.Message);
         }

         return BadRequest(result.Message);

      }
      [HttpGet("[action]")]
      public async Task<IActionResult> GetAllCollectionGroup()
      {
         var result = await _collectionGroupService.GetListCollectionGroup();
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }

         return BadRequest(result.Message);

      }
   }
}
