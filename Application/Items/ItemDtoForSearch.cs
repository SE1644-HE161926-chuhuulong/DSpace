using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Items
{
   public class ItemDtoForSearch
   {
      public int CollectionId { get; set; }
      public string? Title { get; set; }
      public DateTime? StartDateIssue { get; set; }

      public DateTime? EndDateIssue { get; set; }

      public string? Publisher { get; set; }

      public List<string>? Authors { get; set; }
      public List<string>? Keywords { get; set; }

      public List<string>? Types { get; set; }

      public int? Year { get; set; }   

      public int? Month { get; set; }

      public int? Day { get; set; }

      

   }
}
