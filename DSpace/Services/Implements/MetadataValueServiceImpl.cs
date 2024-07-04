using Application.Metadatas;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DSpace.Services.Implements
{
   public class MetadataValueServiceImpl : MetadataValueService
   {
      private IMapper _mapper;
      private readonly DSpaceDbContext _context; 
      public MetadataValueServiceImpl(IMapper mapper,DSpaceDbContext context) { 
         _mapper= mapper;
         _context= context;
      }

      public async Task<bool> AddListMetadataValue(List<MetadataValueDTOForCreate> metadataValueDTO)
      {
         try { 
            List<MetadataValue> listCreate = _mapper.Map<List<MetadataValue>>(metadataValueDTO);
            await _context.MetadataValues.AddRangeAsync(listCreate);
            await _context.SaveChangesAsync();
            return true;
         } 
         catch (Exception ex) 
         { 
            return false;
         }
      }

      public async Task<bool> DeleteListMetadataValue(List<MetadataValueDTOForDelete> metadataValueDTOForDelete)
      {
         try
         {
            List<MetadataValue> list = _mapper.Map<List<MetadataValue>>(metadataValueDTOForDelete);
            _context.MetadataValues.RemoveRange(list);
            await _context.SaveChangesAsync();
            return true;
         }
         catch (Exception ex)
         {
            return false;
         }
      }

      public Task<List<MetadataValueDTOForSelect>> GetMetadataValueByItemId(int metadataValueId)
      {
         throw new NotImplementedException();
      }

      public async Task<bool> UpdateListMetadataValue(List<MetadataValueDTOForUpdate> metadataValueDTO)
      {
         try
         {
            List<MetadataValue> list = _mapper.Map<List<MetadataValue>>(metadataValueDTO);

            foreach (var metadataValueUpdate in list) {
               var metadataValue = await _context.MetadataValues.SingleOrDefaultAsync(x => x.MetadataValueId == metadataValueUpdate.MetadataValueId);
               if(metadataValue != null)
               {
                  metadataValue.TextValue = metadataValueUpdate.TextValue;
                  metadataValue.TextLang= metadataValueUpdate.TextLang;
                  metadataValue.MetadataFieldRegistry = metadataValueUpdate.MetadataFieldRegistry;
               }
            }
            await _context.SaveChangesAsync();
            return true;
         }
         catch (Exception ex)
         {
            return false;
         }
      }
   }
}
