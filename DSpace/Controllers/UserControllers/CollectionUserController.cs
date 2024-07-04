using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers.UserControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CollectionUserController : ControllerBase
   {
      private CollectionService _collectionService;

      public CollectionUserController(CollectionService collectionService)
      {
         _collectionService = collectionService;
      }
      [HttpGet("[action]/{name}")]
      public async Task<IActionResult> GetCollectionByName(string name)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

         var response = await _collectionService.GetCollectionByNameForUser(name, email);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("[action]/{collectionId}")]
      public async Task<IActionResult> GetCollectionById(int collectionId)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

         var response = await _collectionService.GetCollectionByIDForUser(collectionId, email);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("[action]/{communityId}")]
      public async Task<IActionResult> GetCollectionByCommunityId(int communityId)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

         var response = await _collectionService.GetCollectionByCommunityIdForUser(communityId, email);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("[action]/{communityId}")]
      public async Task<IActionResult> GetAllCollection()
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

         var response = await _collectionService.GetAllCollectionForUser(email);
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
