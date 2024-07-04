using Application.Metadatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items
{
   public class ItemDTOForSelectDetailFullForUser
   {
      public int ItemId { get; set; }
      public int CollectionId { get; set; }
      public string CollectionName { get; set; }

      public List<MetadataValueDTOForSelect> ListMetadataValueDTOForSelect { get; set; }
   }
}
