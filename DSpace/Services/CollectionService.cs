using Application.Collections;
using Application.Responses;

namespace DSpace.Services
{
   public interface CollectionService
   {
      public Task<ResponseDTO> GetCollectionByName(string title);
      public Task<ResponseDTO> GetCollectionByID(int collectionId);

      public Task<ResponseDTO> GetAllCollection();

      public Task<ResponseDTO> CreateCollection(CollectionDTOForCreateOrUpdate collectionDTO, int userCreateId);

      public Task<ResponseDTO> UpdateCollection(int collectionId, CollectionDTOForCreateOrUpdate collectionDTO, int userUpdateId);

      public Task<ResponseDTO> DeleteCollection(int collectionId);

      public Task<ResponseDTO> GetCollectionByCommunityId(int communityId);

      public Task<ResponseDTO> GetCollectionByNameForUser(string title,string email);
      public Task<ResponseDTO> GetCollectionByIDForUser(int collectionId,string email);

      public Task<ResponseDTO> GetAllCollectionForUser(string email);

      public Task<ResponseDTO> GetCollectionByCommunityIdForUser(int communityId, string email);

      public Task<ResponseDTO> GetCollectionByPeopleId(int peopleId);
      public Task<ResponseDTO> GetCollectionByCollectionIdAndPeopleId(int collectionId,  int peopleId);
   }
}