using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Group
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GroupId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool isActive { get; set; }
    public List<GroupPeople> GroupPeoples { get; set; }
    public List<CommunityGroup> CommunityGroups { get; set; }
    public List<CollectionGroup> CollectionGroups { get; set; }
}