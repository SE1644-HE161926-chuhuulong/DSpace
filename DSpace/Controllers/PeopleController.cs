using Application.Peoples;
using DSpace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   //[Authorize(Roles ="ADMIN")]
   public class PeopleController : Controller
   {
      private PeopleService _peopleService;
      public PeopleController(PeopleService peopleService)
      {
         _peopleService = peopleService;
      }
      [HttpPost("createPeople")]

      public async Task<IActionResult> CreatePeople(PeopleDTOForCreateUpdate peopleDTO)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userCreateId = int.Parse(idUser);

         var response = await _peopleService.CreatePeople(peopleDTO, userCreateId);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getPeopleById/{id}")]
      public async Task<IActionResult> GetPeopleById(int id)
      {
         var response = await _peopleService.GetPeopleByID(id);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getListOfPeople")]
      public async Task<IActionResult> GetAllPeople()
      {
         var response = await _peopleService.GetAllPeople();
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpPut("updatePeople/{peopleId}")]
      public async Task<IActionResult> UpdatePeople(int peopleId, PeopleDTOForCreateUpdate peopleDTO)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userUpdateId = int.Parse(idUser);

         var response = await _peopleService.UpdatePeople(peopleId, peopleDTO, userUpdateId);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getPeopleByName/{name}")]
      public async Task<IActionResult> GetPeopleByName(string name)
      {
         var response = await _peopleService.GetPeopleByName(name);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getPeopleByEmail/{email}")]
      public async Task<IActionResult> GetPeopleByEmail(string email)
      {
         var response = await _peopleService.GetPeopleByEmail(email);
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
      [Route("[action]/{peopleId}")]
      public async Task<IActionResult> DeletePeople(int peopleId)
      {
         var response = await _peopleService.DeletePeople(peopleId);
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