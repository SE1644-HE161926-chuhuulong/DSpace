using Application.Items;
using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using System.Security.Claims;

namespace DSpace.Controllers.UserControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ItemUserController : ControllerBase
   {
      private readonly ItemService _itemService;

      public ItemUserController(ItemService itemService)
      {
         _itemService = itemService;
      }

      [HttpGet("[action]/{itemId}")]
      public async Task<IActionResult> GetItemFullByIdForUser(int itemId)
      {
         var result = await _itemService.GetItemFullByIdForUser(itemId);
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
      public async Task<IActionResult> GetItemSimpleByIdForUser(int itemId)
      {
         var result = await _itemService.GetItemSimpleByIdForUser(itemId);
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }
         else
         {
            return BadRequest(result);
         }
      }
      [HttpGet("[action]/{collectionId}/{pageIndex}/{pageSize}")]

      public async Task<IActionResult> GetListItemByCollectionIdForUser(int collectionId,int pageIndex,int pageSize)
      {
         var result = await _itemService.GetAllItemByCollectionIdForUser(collectionId,pageIndex,pageSize);
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }
         else
         {
            return BadRequest(result);
         }
      }
      [HttpGet("[action]")]

      public async Task<IActionResult> GetListItemRecentSubmitedForUser()
      {

         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;
         var result = await _itemService.GetListItemRecentForUser(email);
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }
         else
         {
            return BadRequest(result);
         }
      }

      [HttpPost("[action]/{pageIndex}/{pageSize}")]
      public async Task<IActionResult> SearchItem(ItemDtoForSearch itemDtoForSearch,int pageIndex,int pageSize)
      {
         var response = await _itemService.SearchItemForUser(itemDtoForSearch,pageIndex,pageSize);
         if (response.IsSuccess) { 
         return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response);
         }
      }
      [HttpGet("[action]/title")]
      public async Task<IActionResult> SearchItemByTitle(string title)
      {
         var response = await _itemService.SearchItemByTitleForUser(title);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response);
         }
      }
      [HttpGet("[action]/{collectionId}")]
      public async Task<IActionResult> Get5ItemRecentInAcollection(int collectionId)
      {
         var response = await _itemService.Get5ItemRecentlyInOneCollectionForUser(collectionId);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response);
         }
      }
   }
}
