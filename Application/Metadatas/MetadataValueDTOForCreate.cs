using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Metadatas
{
   public class MetadataValueDTOForCreate
   {
      public string? TextValue { get; set; }
      public string? TextLang { get; set; }

      public int MetadataFieldId { get; set; }

      public int ItemId { get; set; }
   }
}
