namespace Application.FileUploads;

public class ModifyFileUploadDto
{
    public string FileUrl { get; set; }
    public string FileKeyId { get; set; }
    public string MimeType { get; set; }
    public string Kind { get; set; }
    public string FileName { get; set; }
    public DateTime CreationTime { get; set; }
    public int ItemId { get; set; }
    public bool isActive { get; set; }

}