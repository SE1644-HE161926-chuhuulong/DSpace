using Application.CommunityGroups;
using Application.Peoples;
using Application.CollectionGroups;

namespace Application.Groups
{
   public class GroupDTOForDetail
   {
      public int GroupId { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public bool isActive { get; set; }
      public List<PeopleDTOForSelect> listPeopleInGroup { get; set; }

      public List<CommunityGroupDTOForSelectList> listCommunityGroup { get; set; }

      public List<CollectionGroupDTOForDetail> listCollectionGroup { get; set; }
   }
}
