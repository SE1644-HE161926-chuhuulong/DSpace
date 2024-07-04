using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class SubcribeController : ControllerBase
   {
      private SubcribeService _subcribeService;

      public SubcribeController(SubcribeService subcribeService)
      {
         _subcribeService = subcribeService;
      }

      [HttpGet("[action]")]
      public async Task<IActionResult> GetListOfSubcribes() {
         var response = await _subcribeService.ViewListSubcribe();
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else { 
            return BadRequest(response.Message);
         }
      }

      [HttpDelete("[action]")]
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
   }
}
