using Domain;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommunityGroups
{
   public class CommunityGroupDTOForDetail
   {
      public int Id { get; set; }
     
      public int CommunityId { get; set; }
      public int GroupId { get; set; }
      public bool isActive { get; set; }
      public bool canReview { get; set; }
      public bool canSubmit { get; set; }
      public bool canEdit { get; set; }
      public string GroupName { get; set; }
      public string CommunityName { get; set; }
   }
}
