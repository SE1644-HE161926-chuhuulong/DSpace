using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers.UserControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class SubcribeUserController : ControllerBase
   {
      private SubcribeService _subcribeService;

      public SubcribeUserController(SubcribeService subcribeService)
      {
         _subcribeService = subcribeService;
      }
      [HttpPost("[action]")]
      public async Task<IActionResult> SubcribeCollection(int collectionId)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

            var response = await _subcribeService.SubcribeCollection(collectionId, email);
            if (response.IsSuccess)
            {
                return Ok(response.ObjectResponse);
            }
            else
            {
                return BadRequest(response.Message);
            }

      }
      [HttpPost("[action]")]
      public async Task<IActionResult> UnSubcribeCollection(int collectionId)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

            var response = await _subcribeService.UnSubcribeCollection(collectionId, email);
            if (response.IsSuccess)
            {
                return Ok(response.ObjectResponse);
            }
            else
            {
                return BadRequest(response.Message);
            }

      }
      [HttpGet("[action]")]
      public async Task<IActionResult> ViewListCollectionSubcribed()
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;

         var response = await _subcribeService.ViewListSubcribeForUser(email);
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
