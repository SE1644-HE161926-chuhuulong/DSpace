using Application.Metadatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items
{
   public class ItemDTOForSelectDetailFull
   {
      public int ItemId { get; set; }
      public DateTime LastModified { get; set; }
      public bool Discoverable { get; set; }
      public int? SubmitterId { get; set; }
      public int CollectionId { get; set; }

      public string CollectionName { get; set; }

      public List<MetadataValueDTOForSelect> ListMetadataValueDTOForSelect { get; set; }
   }
}
