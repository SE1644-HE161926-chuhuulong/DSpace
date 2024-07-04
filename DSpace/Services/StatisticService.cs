using Application.Responses;
using Application.Statistics;

namespace DSpace.Services
{
   public interface StatisticService
   {
      public Task<bool> IncreaseView(int itemId,DateTime dateAccess);

      public Task<ResponseDTO> getAllStatistics();

      public Task<ResponseDTO> getAllStatisticByItemId(int itemId);

      public Task<StatisticDTOForSelectList> getAllStatisticByItemId2(int itemId);


   }
}
