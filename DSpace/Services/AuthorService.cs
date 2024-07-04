using Application.Authors;
using Application.Responses;

namespace DSpace.Services
{
   public interface AuthorService
   {
      public Task<ResponseDTO> GetAuthorByName(string name);
      public Task<ResponseDTO> GetAuthorByID(int authorId);

      public Task<ResponseDTO> GetAllAuthor();

      public Task<ResponseDTO> CreateAuthor(AuthorDTOForCreateUpdate authorDTO);

      public Task<ResponseDTO> UpdateAuthor(int authorId, AuthorDTOForCreateUpdate authorDTO);

      public Task<ResponseDTO> DeleteAuthor(int authorId);
   }
}
