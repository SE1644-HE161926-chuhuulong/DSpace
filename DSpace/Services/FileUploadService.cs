using Application.Responses;

namespace DSpace.Services;

public interface FileUploadService
{
    public Task<ResponseDTO> DownloadFileWithId(string fileId);
    public Task<ResponseDTO> GetDataFileWithId(string fileId);
}