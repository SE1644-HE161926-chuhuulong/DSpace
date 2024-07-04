using Application.CollectionGroups;
using Application.Responses;
using Domain;

namespace DSpace.Services
{
   public interface CollectionGroupService
   {
      public Task<ResponseDTO> AddCollectionManageGroup(CollectionGroupDTOForCreate collectionGroupDTO);

      public Task<ResponseDTO> UpdateCollectionManageGroup(CollectionGroupDTOForUpdate collectionGroupDTO);

      public Task<ResponseDTO> DeleteCollectionManageInGroup(int id);

      public Task<List<Group>> GetListGroupByCollectionId(int collectionId);

      public Task<List<Collection>> GetListCollectionByGroupId(int groupId);

      public Task<ResponseDTO> GetListCollectionGroup();

      public Task<CollectionGroup> GetCollectionGroupByGroupIdAndCollectionId(int collectionId, int groupId);
   }
}
