using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Application.CollectionSubcribes
{
    public class SubcribeDTO
    {
      public int CollectionId { get; set; }
      public string CollectionName { get; set; }

      public double SubcribeAmount { get; set; }
   }
}
