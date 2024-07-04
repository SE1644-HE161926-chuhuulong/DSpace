using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CollectionGroups
{
   public class CollectionGroupDTOForCreate
   {

      public int CollectionId { get; set; }
      public int GroupId { get; set; }
      public bool isActive { get; set; }
      public bool canReview { get; set; }
      public bool canSubmit { get; set; }
      public bool canEdit { get; set; }
   }
}
