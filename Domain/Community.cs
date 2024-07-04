using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Community
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommunityId { get; set; }
    public string LogoUrl { get; set; }
    public string CommunityName { get; set; }
    public string ShortDescription { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public int? CreateBy { get; set; }
    public int? UpdateBy { get; set; }
    public bool isActive { get; set; }
    public int? ParentCommunityId { get; set; }
    public Community ParentCommunity { get; set; }
    public People People { get; set; }
    public List<Community> SubCommunities { get; set; }
    public List<Collection> Collections { get; set; }
    public List<CommunityGroup> CommunityGroups { get; set; }
}