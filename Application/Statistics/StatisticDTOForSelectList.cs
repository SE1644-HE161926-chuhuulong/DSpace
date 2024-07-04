using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Statistics
{
   public class StatisticDTOForSelectList
   {
      public List<StatisticDTOForSelect> Statistics { get; set; }
      public double TotalView { get; set; }

      public string ItemTitle { get; set; }  
   }
}
