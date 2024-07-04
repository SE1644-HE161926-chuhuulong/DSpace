using Application.Collections;
using Domain;
using DSpace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   //[Authorize]
   public class CollectionController : Controller
   {
      private CollectionService _collectionService;

      public CollectionController(CollectionService collectionService)
      {
         _collectionService = collectionService;
      }

      [HttpGet("getListOfCollections")]
      public async Task<IActionResult> GetCollections()
      {
         var response = await _collectionService.GetAllCollection();
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getCollection/{id}")]
      public async Task<IActionResult> GetCollectionById(int id)
      {
         var response = await _collectionService.GetCollectionByID(id);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }

      [HttpGet("getListOfCollectionByCommunityId/{id}")]
      public async Task<IActionResult> GetCollectionByCommunityId(int id)
      {
         var response = await _collectionService.GetCollectionByCommunityId(id);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }

      [HttpPost("createCollection")]
      [Authorize]

      public async Task<IActionResult> CreateCollection(CollectionDTOForCreateOrUpdate collectionDTO)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userCreateId = int.Parse(idUser);

         var response = await _collectionService.CreateCollection(collectionDTO, userCreateId);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }

      [HttpPut("updateCollection/{id}")]
      
      public async Task<IActionResult> UpdateCollection(int id, CollectionDTOForCreateOrUpdate collectionDTO)
      {
         
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userUpdateId = int.Parse(idUser);
         var response = await _collectionService.UpdateCollection(id, collectionDTO, userUpdateId);
         if (response.IsSuccess)
         {
            return Ok(response.Message);
         }
         else
         {
            return BadRequest(response.Message);
         }
         
        
      }
      [HttpGet("getCollectionByName/{name}")]
      public async Task<IActionResult> GetCollectionByName(string name)
      {
         var response = await _collectionService.GetCollectionByName(name);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpDelete]
      [Route("DeleteCollection/{collectionId}")]
      public async Task<IActionResult> DeleteCollection(int collectionId)
      {
         var response = await _collectionService.DeleteCollection(collectionId);
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