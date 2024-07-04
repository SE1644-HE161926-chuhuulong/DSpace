using System.Text;
using Application.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Infrastructure;

namespace DSpace.Services.Implements;

public class FileUploadServiceImpl : FileUploadService
{
    private readonly DSpaceDbContext _context;
    protected ResponseDTO _response;
    private static readonly string[] Scopes = new[] { DriveService.Scope.DriveFile };

    public FileUploadServiceImpl(DSpaceDbContext context)
    {
        _context = context;
        _response = new ResponseDTO();
    }
    
    public async Task<UserCredential> GetUserCredential()
    {
        UserCredential credential = null;
        using (var stream =
               new FileStream(
                   "client_secret_336699035226-8c0s3uv2pee71adqarpuiicagplfpkvj.apps.googleusercontent.com.json",
                   FileMode.Open, FileAccess.ReadWrite))
        {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore("DSpace v2.0", true));
        }

        return credential;
    }
    public async Task<ResponseDTO> DownloadFileWithId(string fileId)
    {
        try
        {
            var data = await GetFileWithSpecificUrl(GetUserCredential(), fileId);
            if (data.Length > 0)
            {
                _response.Message = "Item download successful";
                _response.IsSuccess = true;
                _response.ObjectResponse = data;
            }else
            {
                _response.Message = "Encounter an error when download item";
                _response.IsSuccess = false;
            }

        }
        catch (Exception e)
        {
            _response.Message = e.Message;
            _response.IsSuccess = false;
        }
        return _response;
    }

    public async Task<ResponseDTO> GetDataFileWithId(string fileId)
    {
        try
        {
            var data = await GetData(GetUserCredential(), fileId);
            if (data.Length > 0)
            {
                _response.Message = "Item found";
                _response.IsSuccess = true;
                _response.ObjectResponse = data;
            }else
            {
                _response.Message = "Encounter an error when get item";
                _response.IsSuccess = false;
            }
        }
        catch (Exception e)
        {
            _response.Message = e.Message;
            _response.IsSuccess = false;
        }
        
        return _response;
    }
    
    public async Task<string> GetFileWithSpecificUrl(Task<UserCredential> credential, string fileId)
    {
        try
        {
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = await credential
            });
            var request = service.Files.Get(fileId);
            var fileName = request.Execute().Name;
            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                fileName);
            
            var stream = new MemoryStream();
            request.MediaDownloader.ProgressChanged +=
                progress =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                        case DownloadStatus.Completed:
                        {
                            SaveStream(stream, filePath);
                            Console.WriteLine("Download complete.");
                            break;
                        }
                        case DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                    }
                };
            request.Download(stream);
            return filePath;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void SaveStream(MemoryStream stream, string filePath)
    {
        using (FileStream file = new FileStream(filePath,FileMode.Create, FileAccess.ReadWrite))
        {
            stream.WriteTo(file);
        }
    }
    
    public async Task<byte[]> GetData(Task<UserCredential> credential, string fileId)
    {
        try
        {
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = await credential
            });
            var request = service.Files.Get(fileId);
            var stream = new MemoryStream();
            request.MediaDownloader.ProgressChanged +=
                progress =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                        case DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            break;
                        }
                        case DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                    }
                };
            request.Download(stream);
            
            return stream.ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}