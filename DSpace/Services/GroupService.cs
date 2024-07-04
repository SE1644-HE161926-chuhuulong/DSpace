using Application.Groups;
using Application.Responses;
namespace DSpace.Services
{
   public interface GroupService
   {
      public Task<ResponseDTO> GetGroupByTitle(string title);
      public Task<ResponseDTO> GetGroupByID(int groupId);
      public Task<ResponseDTO> GetGroupByIDForStaff(int groupId, int peopleId);

      public Task<ResponseDTO> GetAllGroup();

      public Task<ResponseDTO> CreateGroup(GroupDTOForCreateUpdate groupDTO);

      public Task<ResponseDTO> UpdateGroup(int groupId, GroupDTOForCreateUpdate groupDTO);

      public Task<ResponseDTO> DeleteGroup(int groupId);

      public Task<ResponseDTO> GetGroupByPeopleId(int peopleId);
   }
}
