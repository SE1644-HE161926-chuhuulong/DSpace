using Application.Responses;

namespace DSpace.Services
{
   public interface SubcribeService
   {
      public Task<ResponseDTO> SubcribeCollection(int collectionId,string userId);

      public Task<ResponseDTO> UnSubcribeCollection(int collectionId, string userId);

      public Task<bool> CheckSubcribe(int collectionId, string email);

      public Task<ResponseDTO> ViewListSubcribe();

      public Task<ResponseDTO> ViewListSubcribeForUser(string email);

      public Task<IEnumerable<string>> GetListUserEmailSubcribeCollection(int collectionId); 


      public Task<List<int>> GetAllCollectionIdByUserEmail(string email);

     

   }
}
