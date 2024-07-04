using Application.Metadatas;
using AutoMapper;
using Domain;
using Infrastructure;

namespace DSpace.Services.Implements
{
   public class MetadataFieldRegistryServiceImpl : MetadataFieldRegistryService
   {
      private IMapper _mapper;
      private readonly DSpaceDbContext _context;

      public MetadataFieldRegistryServiceImpl(DSpaceDbContext context,IMapper mapper)
      {
         _context = context;
         _mapper = mapper;
      }

      public List<MetadateFieldDTO> GetMetadataFieldRegistries()
      {
         return _mapper.Map<List<MetadateFieldDTO>>(_context.MetadataFieldRegistries);
      }
   }
}
