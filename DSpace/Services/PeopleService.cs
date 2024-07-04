using Application.Peoples;
using Application.Responses;

namespace DSpace.Services
{
   public interface PeopleService
   {
      public Task<ResponseDTO> GetPeopleByID(int id);
      public Task<ResponseDTO> GetPeopleByName(string name);

      public Task<ResponseDTO> GetPeopleByEmail(string email);

      public Task<ResponseDTO> GetAllPeople();

      public Task<ResponseDTO> CreatePeople(PeopleDTOForCreateUpdate peopleDTO, int userCreateId);

      public Task<ResponseDTO> UpdatePeople(int id, PeopleDTOForCreateUpdate peopleDTO, int userUpdateId);

      public Task<ResponseDTO> DeletePeople(int id);


   }
}