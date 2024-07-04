using Application.Metadatas;
using Domain;

namespace DSpace.Services
{
   public interface MetadataFieldRegistryService
   {
      public List<MetadateFieldDTO> GetMetadataFieldRegistries();
   }
}
