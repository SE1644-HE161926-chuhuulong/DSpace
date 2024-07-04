namespace Domain;

public class MetadataValue
{
    public int MetadataValueId { get; set; }
    public string TextValue { get; set; }
    public string TextLang { get; set; }
    public int MetadataFieldId { get; set; }
    public int ItemId { get; set; }
    public MetadataFieldRegistry MetadataFieldRegistry { get; set; }
    public Item Item { get; set; }
}