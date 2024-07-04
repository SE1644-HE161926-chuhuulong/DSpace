using Application.Communities;
using Application.Responses;

namespace DSpace.Services
{
   public interface CommunityService
   {
      public Task<ResponseDTO> GetCommunityByName(string title);
      public Task<ResponseDTO> GetCommunityByID(int communityId);

      public Task<ResponseDTO> GetAllCommunity();

      public Task<ResponseDTO> CreateCommunity(CommunityDTOForCreateOrUpdate communityDTO, int userCreateId);

      public Task<ResponseDTO> UpdateCommunity(int communityId, CommunityDTOForCreateOrUpdate communityDTO, int userUpdateId);

      public Task<ResponseDTO> DeleteCommunity(int communityId);

      public Task<ResponseDTO> GetCommunityByParentId(int communityParentId);

      public Task<ResponseDTO> GetAllCommunityForUser();

      public Task<ResponseDTO> GetCommunityByParentIdForUser(int communityParentId);

      public Task<ResponseDTO> GetCommunityByNameForUser(string title);
      public Task<ResponseDTO> GetCommunityByIDForUser(int communityId);

      public Task<ResponseDTO> GetCommunityByPeopleId(int peopleId);

      public Task<ResponseDTO> GetAllCommunityParent();


   }
}