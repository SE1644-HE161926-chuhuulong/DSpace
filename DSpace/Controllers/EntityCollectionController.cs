using DSpace.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EntityCollectionController : Controller
{
    private EntityCollectionService _entityCollectionService;

    public EntityCollectionController(EntityCollectionService entityCollectionService)
    {
        _entityCollectionService = entityCollectionService;
    }

    [HttpGet("getEntityCollection/{entityId}")]
    public async Task<IActionResult> GetEntityCollectionById(int entityId)
    {
        var response = await _entityCollectionService.GetEntityCollectionByID(entityId);
        if (response.IsSuccess)
        {
            return Ok(response.ObjectResponse);
        }
        else
        {
            return BadRequest(response.Message);
        }
    }

    [HttpGet("listEntityCollection")]
    public async Task<IActionResult> GetAllEntityCollections()
    {
        var response = await _entityCollectionService.GetAllEntityCollections();
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