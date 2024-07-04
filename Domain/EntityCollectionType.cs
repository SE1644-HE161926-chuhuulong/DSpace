namespace Domain;

public class EntityCollectionType
{
    public int Id { get; set; }
    public string EntityType { get; set; }
    public List<Collection> Collection { get; set; }
}