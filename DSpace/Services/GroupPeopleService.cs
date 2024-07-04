using Application.GroupPeoples;
using Application.Responses;
using Domain;

namespace DSpace.Services
{
   public interface GroupPeopleService
   {
      public Task<ResponseDTO> AddPeopleInGroup(GroupPeopleDTO groupPeopleDTO);
      public Task<ResponseDTO> DeletePeopleInGroup(GroupPeopleDTO groupPeopleDTO);

      public Task<ResponseDTO> AddListPeopleInGroup(GroupPeopleListDTOForCreateUpdate groupPeopleListDTO);

      public Task<ResponseDTO> UpdateListPeopleInGroup(GroupPeopleListDTOForCreateUpdate groupPeopleListDTO);

      public Task<ResponseDTO> DeleteListPeopleInGroup(int groupId);

      public Task<List<Group>> GetListGroupByPeopleId(int peopleId);

      public Task<List<People>> GetListPeopleByGroupId(int groupId);







   }
}
