using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace Domain;

public class Collection
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CollectionId { get; set; }
    public string LogoUrl { get; set; }
    public string CollectionName { get; set; }
    public string ShortDescription { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public int? CreateBy { get; set; }
    public int? UpdateBy { get; set; }
    public int? CommunityId { get; set; }
    public bool isActive { get; set; }
    public string License { get; set; }
    public string? FolderId { get; set; }
    public People People { get; set; }
    public int EntityTypeId { get; set; }
    public EntityCollectionType EntityType { get; set; }
    public Community Community { get; set; }
    public List<Item> Items { get; set; }
    public List<CollectionGroup> CollectionGroups { get; set; }
    public List<Subscribe> Subscribes { get; set; }
}