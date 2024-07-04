using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GroupPeoples
{
   public class GroupPeopleListDTOForCreateUpdate
   {
      public int GroupId { get; set; }
      public List<int> ListPeopleId { get; set; }

   }
}
