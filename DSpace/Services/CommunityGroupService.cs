using Application.CommunityGroups;
using Application.Responses;
using Domain;

namespace DSpace.Services
{
   public interface CommunityGroupService
   {
      public Task<ResponseDTO> AddCommunityManageGroup(CommunityGroupDTOForCreate communityGroupDTO);
      public Task<ResponseDTO> UpdateCommunityManageGroup(CommunityGroupDTOForUpdate communityGroupDTO);

      public Task<ResponseDTO> DeleteCommunityManageInGroup(int id);


      public Task<List<Group>> GetListGroupByCommunityId(int communityId);

      public Task<List<Community>> GetListCommunityByGroupId(int groupId);

      public Task<ResponseDTO> GetListCommunityGroup();
   }
}
