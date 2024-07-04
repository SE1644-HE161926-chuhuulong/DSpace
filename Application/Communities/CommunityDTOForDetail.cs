using Application.CommunityGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Communities
{
   public class CommunityDTOForDetail
   {
      public int CommunityId { get; set; }
      public string LogoUrl { get; set; }
      public string CommunityName { get; set; }
      public string ShortDescription { get; set; }
      public DateTime CreateTime { get; set; }
      public DateTime UpdateTime { get; set; }
      public string? CreateBy { get; set; }
      public string? UpdateBy { get; set; }
      public bool isActive { get; set; }
      public int? ParentCommunityId { get; set; }

      public List<CommunityGroupDTOForDetail> communityGroupDTOForDetails { get; set; }
   }
}
