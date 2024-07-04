using Application.Responses;
using Application.Statistics;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Services.Implements
{
   public class StatisticServiceImpl : StatisticService
   {
      private readonly DSpaceDbContext _context;
      protected IMapper _mapper;
      protected ResponseDTO _response;


      public StatisticServiceImpl(DSpaceDbContext context, IMapper mapper)
      {
         _context = context;
         _mapper = mapper;

         _response = new ResponseDTO();
      }

      public async Task<ResponseDTO> getAllStatistics()
      {
         try
         {
            var statistics = await _context.Statistics.ToListAsync();

            var listItem = statistics.GroupBy(x => x.ItemId).ToList();

            var listItemId = listItem.Select(x => x.Key).ToList();

            List<StatisticDTOForSelectList> result = new List<StatisticDTOForSelectList>();

            foreach (var itemId in listItemId)
            {


               var itemStatic = await getAllStatisticByItemId2(itemId);
               result.Add(itemStatic);
            }
            _response.IsSuccess = true;
            _response.ObjectResponse = result;

         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<bool> IncreaseView(int itemId, DateTime dateAccess)
      {
         try
         {
            var statisticCheck = await _context.Statistics.SingleOrDefaultAsync(x => x.Month == dateAccess.Month && x.Year == dateAccess.Year && x.ItemId == itemId);
            if (statisticCheck == null)
            {
               Statistic statisticNew = new Statistic();
               statisticNew.Month = dateAccess.Month;
               statisticNew.Year = dateAccess.Year;
               statisticNew.ViewCount = 1;
               statisticNew.ItemId = itemId;
               _context.Statistics.Add(statisticNew);
            }
            else
            {
               statisticCheck.ViewCount += 1;
               _context.Statistics.Update(statisticCheck);

            }
            _context.SaveChanges();
            return true;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }
      public async Task<ResponseDTO> getAllStatisticByItemId(int itemId)
      {
         try
         {
            StatisticDTOForSelectList result = new StatisticDTOForSelectList();
            var statistic = await _context.Statistics.Where(x => x.ItemId == itemId).ToListAsync();


            var item = await _context.Items.Include(x => x.MetadataValue).FirstOrDefaultAsync(x => x.ItemId == itemId);

            result.ItemTitle = item.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57) == null ?
               "" : item.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57).TextValue;
            result.Statistics = _mapper.Map<List<StatisticDTOForSelect>>(statistic);
            result.TotalView = 0;
            foreach (var s in result.Statistics)
            {
               result.TotalView += s.ViewCount;
            }

            _response.IsSuccess = true;
            _response.ObjectResponse = result;
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;

      }
      public async Task<StatisticDTOForSelectList> getAllStatisticByItemId2(int itemId)
      {
         try
         {
            StatisticDTOForSelectList result = new StatisticDTOForSelectList();
            var statistic = await _context.Statistics.Where(x => x.ItemId == itemId).ToListAsync();


            var item = await _context.Items.Include(x => x.MetadataValue).FirstOrDefaultAsync(x => x.ItemId == itemId);

            result.ItemTitle = item.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57) == null ?
               "" : item.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57).TextValue;
            result.Statistics = _mapper.Map<List<StatisticDTOForSelect>>(statistic);
            result.TotalView = 0;
            foreach (var s in result.Statistics)
            {
               result.TotalView += s.ViewCount;
            }
            return result;

         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }


      }
   }
}
