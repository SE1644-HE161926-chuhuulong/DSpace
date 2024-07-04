namespace Domain;

public class Subscribe
{
    public string UserId { get; set; }
    public int CollectionId { get; set; }
    public User User { get; set; }
    public Collection Collection { get; set; }
}