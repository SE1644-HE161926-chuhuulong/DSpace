namespace Domain;

public class AuthorItem
{
    public int ItemId { get; set; }
    public int AuthorId { get; set; }
    public Item Item { get; set; }
    public Author Author { get; set; }
}