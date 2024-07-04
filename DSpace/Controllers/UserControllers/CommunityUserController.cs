﻿using DSpace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers.UserControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CommunityUserController : ControllerBase
   {
      private CommunityService _communityService;

      public CommunityUserController(CommunityService communityService)
      {
         _communityService = communityService;
      }

      [HttpGet("getListOfCommunities")]
      public async Task<IActionResult> GetCommunities()
      {
         var response = await _communityService.GetAllCommunityForUser();
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
         var response = await _communityService.GetCommunityByIDForUser(id);
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
         var response = await _communityService.GetCommunityByParentIdForUser(id);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getCommunityByName/{name}")]
      public async Task<IActionResult> GetCommunityByName(string name)
      {
         var response = await _communityService.GetCommunityByNameForUser(name);
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
