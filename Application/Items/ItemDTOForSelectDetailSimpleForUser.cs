using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items
{
   public class ItemDTOForSelectDetailSimpleForUser
   {
      public int itemId { get; set; }
      //no 1
      public List<string> Authors { get; set; }
      // 57
      public string? Title { get; set; }
      //12
      public DateTime? DateOfIssue { get; set; }
      //36
      public string? Publisher { get; set; }
      //51 
      public List<string> SubjectKeywords { get; set; }
      //15
      public string? Abstract { get; set; }
      //34
      public string? IdentifierUri { get; set; }
      //14
      public string? Description { get; set; }

      public int CollectionId { get; set; }
      public string? CollectionName { get; set; }
   }
}
