using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class StatisticController : ControllerBase
   {
      private StatisticService _statisticService;

      public StatisticController(StatisticService statisticService)
      {
         _statisticService = statisticService;
      }
      [HttpGet("[action]/{itemId}")]
      public async Task<IActionResult> GetStatisticByItemId(int itemId) { 
         var result = await _statisticService.getAllStatisticByItemId(itemId);
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }
         else {
            return BadRequest(result.Message);
         }
      }
      [HttpGet("[action]")]
      public async Task<IActionResult> GetAllStatistic()
      {
         var result = await _statisticService.getAllStatistics();
         if (result.IsSuccess)
         {
            return Ok(result.ObjectResponse);
         }
         else
         {
            return BadRequest(result.Message);
         }
      }
   }
}
