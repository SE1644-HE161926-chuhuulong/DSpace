using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Metadatas
{
   public  class MetadataValueDTOForModified
   {
      public List<MetadataValueDTOForCreate>? Add { get;set; }

      public List<MetadataValueDTOForUpdate>? Update { get; set; }

      public List<MetadataValueDTOForDelete>? Delete { get; set; }




   }
}
