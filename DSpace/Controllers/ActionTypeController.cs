using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ActionTypeController : ControllerBase
   {
      [HttpGet("[action]")]
      public IActionResult GetAllAction()
      {
         string[] actionTypes = Enum.GetNames(typeof(ActionType));

         var v = actionTypes.Select((value, key) =>
         new { value, key }).ToDictionary(x => x.key + 1, x => x.value);

         return Ok(v);
      }
   }
}
