namespace Domain;

public class FileUpload
{
    public int FileId { get; set; }
    public string FileUrl { get; set; }
    public string FileKeyId { get; set; }
    public string MimeType { get; set; }
    public string Kind { get; set; }
    public string FileName { get; set; }
    public DateTime CreationTime { get; set; }
    public bool isActive { get; set; }
    public int DownloadCount { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
}