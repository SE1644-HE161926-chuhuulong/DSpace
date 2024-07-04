namespace Domain;

public class MetadataFieldRegistry
{
    public int MetadataFieldId { get; set; }
    public string Element { get; set; }
    public string Qualifier { get; set; }
    public string ScopeNote { get; set; }
    public List<MetadataValue> MetadataValue { get; set; }
}