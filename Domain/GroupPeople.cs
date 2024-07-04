namespace Domain;

public class GroupPeople
{
    public int GroupId { get; set; }
    public int PeopleId { get; set; }
    public Group Group { get; set; }
    public People People { get; set; }
}