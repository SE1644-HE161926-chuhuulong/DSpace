using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Statistics
{
   public class StatisticDTOForCreate
   {
      public int StatisticId { get; set; }
      public int Month { get; set; }
      public int Year { get; set; }
   
      public int? ItemId { get; set; }
   }
}
