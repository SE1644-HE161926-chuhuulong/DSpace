using DSpace.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSpace.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : Controller
{
    private FileUploadService _fileUploadService;
    public FileUploadController(FileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }
    
    [HttpGet("downloadFile/{fileKeyId}")]
    public async Task<IActionResult> DownloadFileWithKeyId([FromRoute] string fileKeyId)
    {
        var response = await _fileUploadService.DownloadFileWithId(fileKeyId);
        if (response.IsSuccess)
        {
            return Ok(response.ObjectResponse);
        }
        else
        {
            return BadRequest(response.Message);
        }
    }
    
    [HttpGet("getDataFile/{fileKeyId}")]
    public async Task<IActionResult> GetDataFileWithKeyId([FromRoute] string fileKeyId)
    {
        var response = await _fileUploadService.GetDataFileWithId(fileKeyId);
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