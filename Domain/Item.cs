namespace Domain;
//
public class Item
{
    public int ItemId { get; set; }
    public DateTime LastModified { get; set; }
    public bool Discoverable { get; set; }
    public int? SubmitterId { get; set; }
    public int? MetadataId { get; set; }
    public int? LanguageId { get; set; }
    public int CollectionId { get; set; }
    public People People { get; set; }
    public Collection Collection { get; set; }
    public List<AuthorItem> AuthorItems { get; set; }
    public List<FileUpload> File { get; set; } = null!;
    public List<MetadataValue> MetadataValue { get; set; }
    public List<Statistic> Statistics { get; set; }
}