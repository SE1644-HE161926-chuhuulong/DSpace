using Application.Items;
using Application.Metadatas;
using AutoMapper;
using DSpace.Services;
using DSpace.Services.Implements;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ItemController : Controller
{
   private readonly ItemService _itemService;
   private readonly MetadataFieldRegistryService _metadataFieldRegistryService;

   public ItemController(ItemService service, MetadataFieldRegistryService metadataFieldRegistryService)
   {
      _itemService = service;
      _metadataFieldRegistryService = metadataFieldRegistryService;
   }

   [HttpGet("getAllItems/{pageIndex}/{pageSize}")]
   public async Task<ActionResult<List<ItemDto>>> GetAllItem(int pageIndex,int pageSize)
   {
      var response = await _itemService.GetAllItem(pageIndex,pageSize);
      if (response.IsSuccess)
      {
         return Ok(response.ObjectResponse);
      }
      else
      {
         return BadRequest(response.Message);
      }
   }

   [HttpGet("itemsInCollection/{collectionId}/{pageIndex}/{pageSize}")]
   public async Task<ActionResult<List<ItemDto>>> GetAllItemInOneCollection([FromRoute] int collectionId,int pageIndex,int pageSize)
   {
      var response = await _itemService.GetAllItemInOneCollection(collectionId,pageIndex,pageSize);
      if (response.IsSuccess)
      {
         return Ok(response.ObjectResponse);
      }
      else
      {
         return BadRequest(response.Message);
      }
   }

   [HttpDelete("deleteItem/{itemId}")]
   public async Task<ActionResult> DeleteItem([FromRoute] int itemId)
   {
      var response = await _itemService.DeleteItem(itemId);
      if (response.IsSuccess)
      {
         return Ok(response.Message);
      }
      else
      {
         return BadRequest(response.Message);
      }
   }

   [HttpGet("[action]")]
   public IActionResult GetMetadataFieldRegistries()
   {
      return Ok(_metadataFieldRegistryService.GetMetadataFieldRegistries());
   }

   [HttpPost("[action]")]
   [Authorize]
   public async Task<IActionResult> CreateSimpleItem([FromForm] ItemDTOForCreateSimple itemDTO, [FromForm] List<IFormFile> multipleFiles)
   {
      var identity = HttpContext.User.Identity as ClaimsIdentity;

      if (identity == null)
      {
         return BadRequest();
      }
      var userClaims = identity.Claims;
      var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
      int userCreateId = int.Parse(idUser);

      var response = await _itemService.CreateSimpleItem(itemDTO, multipleFiles, userCreateId);
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
      var result = await _itemService.GetItemSimpleById(itemId);
      if (result.IsSuccess)
      {
         return Ok(result.ObjectResponse);
      }
      else { 
         return BadRequest(result);
      }
   }
   [HttpPut("[action]/{itemId}")]
   public async Task<IActionResult> ModifyItem(MetadataValueDTOForModified metadataValueDTOForModified,int itemId)
   {
      var identity = HttpContext.User.Identity as ClaimsIdentity;

      if (identity == null)
      {
         return BadRequest();
      }
      var userClaims = identity.Claims;
      var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
      int userCreateId = int.Parse(idUser);

      var result = await _itemService.ModifyItem(metadataValueDTOForModified,userCreateId,itemId);
      if (result.IsSuccess)
      {
         return Ok(result);
      }
      else
      {
         return BadRequest(result);
      }
   }
   [HttpGet("[action]/{itemId}")]
   public async Task<IActionResult> GetItemFullById(int itemId)
   {
      var result = await _itemService.GetItemFullById(itemId);
      if (result.IsSuccess)
      {
         return Ok(result.ObjectResponse);
      }
      else
      {
         return BadRequest(result);
      }
   }
   [HttpPut("[action]")]
   public async Task<IActionResult> ChangeCollectionOfItem(int itemId, int collectionId)
   {
      var identity = HttpContext.User.Identity as ClaimsIdentity;

      if (identity == null)
      {
         return BadRequest();
      }
      var userClaims = identity.Claims;
      var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
      int userId = int.Parse(idUser);

      var result = await _itemService.UpdateCollection(itemId,collectionId,userId);
      if (result.IsSuccess)
      {
         return Ok(result.ObjectResponse);
      }
      else
      {
         return BadRequest(result);
      }
   }
   [HttpPut("[action]")]
   public async Task<IActionResult> ChangeDiscoverableOfItem(int itemId, bool discoverable)
   {
      var identity = HttpContext.User.Identity as ClaimsIdentity;

      if (identity == null)
      {
         return BadRequest();
      }
      var userClaims = identity.Claims;
      var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
      int userId = int.Parse(idUser);

      var result = await _itemService.UpdateStatus(itemId, discoverable, userId);
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
   public async Task<IActionResult> SearchItem(ItemDtoForSearch itemDtoForSearch, int pageIndex, int pageSize)
   {
      var response = await _itemService.SearchItem(itemDtoForSearch, pageIndex, pageSize);
      if (response.IsSuccess)
      {
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
      var response = await _itemService.SearchItemByTitle(title);
      if (response.IsSuccess)
      {
         return Ok(response.ObjectResponse);
      }
      else
      {
         return BadRequest(response);
      }
   }
   [HttpGet("[action]")]
   public async Task<IActionResult> Get5ItemRecentInACollection(int collectionId)
   {
      var response = await _itemService.Get5ItemRecentlyInOneCollection(collectionId);
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