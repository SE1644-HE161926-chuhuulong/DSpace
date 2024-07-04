namespace Domain;

public class Author
{
    public int AuthorId { get; set; }
    public string FullName { get; set; }
    public string JobTitle { get; set; }
    public DateTime DateAccessioned { get; set; }
    public DateTime DateAvailable { get; set; }
    public string Uri { get; set; }
    public string Type { get; set; }
    public List<AuthorItem> AuthorItems { get; set; }
}