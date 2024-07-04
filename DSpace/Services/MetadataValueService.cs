using Application.Metadatas;

namespace DSpace.Services
{
   public interface MetadataValueService
   {
      public Task<bool> AddListMetadataValue(List<MetadataValueDTOForCreate> metadataValueDTO);
      public Task<bool> UpdateListMetadataValue(List<MetadataValueDTOForUpdate> metadataValueDTO);

      public Task<bool> DeleteListMetadataValue(List<MetadataValueDTOForDelete> metadataValueDTOForDelete);
      public Task<List<MetadataValueDTOForSelect>> GetMetadataValueByItemId(int metadataValueId);




   }
}
