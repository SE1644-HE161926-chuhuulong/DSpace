using Application.Communities;
using Application.CollectionGroups;

namespace Application.Collections
{
   public class CollectionDTOForDetail
   {
      public int CollectionId { get; set; }
      public string LogoUrl { get; set; }
      public string CollectionName { get; set; }
      public string ShortDescription { get; set; }
      public DateTime CreateTime { get; set; }
      public DateTime UpdateTime { get; set; }
      public string? CreateBy { get; set; }
      public string? UpdateBy { get; set; }
      public int CommunityId { get; set; }
      public bool isActive { get; set; }
      public string License { get; set; }
      public string EntityTypeName { get; set; }

      public CommunityDTOForSelect CommunityDTOForSelect { get; set; }

      public List<CollectionGroupDTOForDetail> CollectionGroupDTOForDetails { get; set; }
   }
}
