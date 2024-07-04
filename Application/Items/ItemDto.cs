namespace Application.Items;

public class ItemDto
{
    public int ItemId { get; set; }
    public DateTime LastModified { get; set; }
    public bool Discoverable { get; set; }
    public int SubmitterId { get; set; }
    public int MetadataId { get; set; }
    public int LanguageId { get; set; }
    public int CollectionId { get; set; }
}