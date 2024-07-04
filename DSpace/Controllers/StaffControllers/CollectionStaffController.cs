using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers.StaffControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CollectionStaffController : ControllerBase
   {
      private CollectionService _collectionService;

      public CollectionStaffController(CollectionService collectionService)
      {
         _collectionService = collectionService;
      }
      [HttpGet("[action]")]
      public async Task<IActionResult> GetAllCollections()
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;

         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         var userId = int.Parse(idUser);

         var result = await _collectionService.GetCollectionByPeopleId(userId);
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
      [HttpGet("getCollection/{id}")]
      public async Task<IActionResult> GetCollectionById(int id)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;

         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         var userId = int.Parse(idUser);
         var response = await _collectionService.GetCollectionByCollectionIdAndPeopleId(id,userId);
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
