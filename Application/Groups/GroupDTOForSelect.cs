using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups
{
   public class GroupDTOForSelect
   {
      public int GroupId { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public bool isActive { get; set; }
   }
}
