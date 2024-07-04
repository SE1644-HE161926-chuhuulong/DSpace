using Application.Authors;
using Domain;
using DSpace.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthorController : Controller
   {
      private AuthorService _authorService;

      public AuthorController(AuthorService authorService)
      {
         _authorService = authorService;
      }

      [AllowAnonymous]
      [HttpGet("getListOfAuthors")]
      public async Task<IActionResult> GetAuthors()
      {
         var response = await _authorService.GetAllAuthor();
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [AllowAnonymous]
      [HttpGet("getAuthor/{id}")]
      public async Task<IActionResult> GetAuthor(int id)
      {
         var response = await _authorService.GetAuthorByID(id);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [AllowAnonymous]
      [HttpPost("createAuthor")]

      public async Task<IActionResult> CreateAuthor(AuthorDTOForCreateUpdate authorDTO)
      {
         var response = await _authorService.CreateAuthor(authorDTO);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }

      [HttpPut("updateAuthor/{id}")]
      public async Task<IActionResult> UpdateAuthor(int id, AuthorDTOForCreateUpdate request)
      {
         var response = await _authorService.UpdateAuthor(id, request);
         if (response.IsSuccess)
         {
            return Ok(response.Message);
         }
         else
         {
            return BadRequest(response.Message);
         }
      }
      [HttpGet("getAuthorByName/{name}")]
      public async Task<IActionResult> GetAuthorByName(string name)
      {
         var response = await _authorService.GetAuthorByName(name);
         if (response.IsSuccess)
         {
            return Ok(response.ObjectResponse);         }
         else
         {
            return BadRequest(response.Message);
         }
      }

      [HttpDelete]
      [Route("[action]/{authorId}")]
      public async Task<IActionResult> DeleteAuthor(int authorId) { 
         var response = await _authorService.DeleteAuthor(authorId);
         if (response.IsSuccess)
         {
            return Ok(response.Message);
         }
         else { 
            return BadRequest(response.Message);
         }
      }
   }
}
