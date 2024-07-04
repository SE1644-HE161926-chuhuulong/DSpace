using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Statistics
{
   public class StatisticDTOForSelect
   {

      public int StatisticId { get; set; }

      public string MonthYear { get; set; }
      public int? ItemId { get; set; }

      public double ViewCount { get; set; }
   }
}
