using Application.Items;
using Application.Metadatas;
using Domain;
using DSpace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers.StaffControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ItemStaffController : ControllerBase
   {
      private ItemService _itemService;
      private CollectionService _collectionService;
      private CollectionGroupService _collectionGroupService;
      private GroupPeopleService _groupPeopleService;

      public ItemStaffController(ItemService itemService, 
         CollectionService collectionService, 
         CollectionGroupService collectionGroupService, 
         GroupPeopleService groupPeopleService)
      {
         _itemService = itemService;
         _collectionService = collectionService;
         _collectionGroupService = collectionGroupService;
         _groupPeopleService = groupPeopleService;
      }

      [HttpGet("[action]/{pageIndex,pageSize}")]
      public async Task<IActionResult> GetAllItems(int pageIndex,int pageSize)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;

         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         var userId = int.Parse(idUser);
         var result = await _itemService.GetAllItemByPeopleId(userId,pageIndex,pageSize);
         if (result.IsSuccess)
         {
            if (result.ObjectResponse != null)
            {
               return Ok(result.ObjectResponse);
            }
            return NotFound(result.Message);

         }
         else
         {
            return BadRequest(result.Message);
         }
      }
      [HttpPost("[action]")]
      [Authorize]
      public async Task<IActionResult> CreateSimpleItem(ItemDTOForCreateSimple itemDTO)
      {
         

         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userId = int.Parse(idUser);

        
         var response = await _itemService.CreateSimpleItemByStaff(itemDTO, userId);
         if (response.IsSuccess)
         {
            return Ok(response);
         }
         else
         {
            return BadRequest(response);
         }

      }
      [HttpGet("[action]/{itemId}")]
      public async Task<IActionResult> GetItemSimpleById(int itemId)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userId = int.Parse(idUser);
         var result = await _itemService.GetItemSimpleByIdByStaff(itemId,userId);
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }
         else
         {
            return BadRequest(result);
         }
      }
      [HttpPut("[action]/{itemId}")]
      public async Task<IActionResult> ModifyItem(MetadataValueDTOForModified metadataValueDTOForModified, int itemId)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userCreateId = int.Parse(idUser);

         var result = await _itemService.ModifyItemByStaff(metadataValueDTOForModified, userCreateId, itemId);
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }
         else
         {
            return BadRequest(result);
         }
      }
      [HttpGet("[action]/{itemId}")]
      public async Task<IActionResult> GetItemFullById(int itemId)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userId = int.Parse(idUser);
         var result = await _itemService.GetItemFullByIdByStaff(itemId,userId);
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }
         else
         {
            return BadRequest(result);
         }
      }
   }
}
