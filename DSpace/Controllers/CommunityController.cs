using Application.Communities;
using Application.Groups;
using Domain;
using DSpace.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CommunityController : Controller
   {
      private CommunityService _communityService;

      public CommunityController(CommunityService communityService)
      {
         _communityService = communityService;
      }

      [HttpGet("getListOfCommunities")]
      public async Task<IActionResult> GetCommunities()
      {
         var response = await _communityService.GetAllCommunity();
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getCommunity/{id}")]
      public async Task<IActionResult> GetCommunityById(int id)
      {
         var response = await _communityService.GetCommunityByID(id);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }

      [HttpGet("getListOfCommunitiesByParentId/{id}")]
      public async Task<IActionResult> GetCommunityByParentId(int id)
      {
         var response = await _communityService.GetCommunityByParentId(id);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }

      [HttpPost("createCommunity")]
      [Authorize]

      public async Task<IActionResult> CreateCommunity([FromBody] CommunityDTOForCreateOrUpdate communityDTO)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;
         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         int userCreateId = int.Parse(idUser);

         var response = await _communityService.CreateCommunity(communityDTO, userCreateId);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }

      [HttpPut("updateCommunity/{id}")]
      //[Authorize]
      public async Task<IActionResult> UpdateCommunity(int id, CommunityDTOForCreateOrUpdate communityDTO)
      {
         var identity = HttpContext.User.Identity as ClaimsIdentity;

         if (identity == null)
         {
            return BadRequest();
         }
         var userClaims = identity.Claims;

         var idUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Authentication).Value;
         var userUpdateId = int.Parse(idUser);

         var response = await _communityService.UpdateCommunity(id, communityDTO, userUpdateId);
         if (response.IsSuccess)
         {
            return Ok(response.Message);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getCommunityByName/{name}")]
      public async Task<IActionResult> GetCommunityByName(string name)
      {
         var response = await _communityService.GetCommunityByName(name);
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
      [Route("[action]/{communityId}")]
      public async Task<IActionResult> DeleteCommunity(int communityId)
      {
         var response = await _communityService.DeleteCommunity(communityId);
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
      public async Task<IActionResult> GetCommunityParent()
      {
         var response = await _communityService.GetAllCommunityParent();
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